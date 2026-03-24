using Core.Adstract;
using Core.MessagingComminication.MessagingDto.Departament;
using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using PetDS.Application.Locations;
using PetDS.Contract.Departamen;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location.VO;
using SharedKernel.Exseption;

namespace PetDS.Application.Departaments.Commands.UpdateDepartament.UpdateDepartamentLocations;

public class UpdateDepartamentLocationsServise : IHandler<Guid, UpdateDepartamentLocationsCommand>
{
    private readonly IDepartamentRepository _departamentRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly ILogger<UpdateDepartamentLocationsServise> _logger;
    private readonly IValidator<UpdateDepartamentLocationsDto> _validator;
    private readonly IConnectionManeger _connectionManeger;

    public UpdateDepartamentLocationsServise(
        IValidator<UpdateDepartamentLocationsDto> validator,
        ILogger<UpdateDepartamentLocationsServise> logger,
        IDepartamentRepository departamentRepository,
        ILocationRepository locationRepository,
        IConnectionManeger connectionManeger)
    {
        _validator = validator;
        _logger = logger;
        _departamentRepository = departamentRepository;
        _locationRepository = locationRepository;
        _connectionManeger = connectionManeger;
    }

    public async Task<Result<Guid, Errors>> Handler(UpdateDepartamentLocationsCommand command,
        CancellationToken cancellationToken = default)
    {
        Task<ValidationResult>? result = _validator.ValidateAsync(command.dto, cancellationToken);
        if (result.IsFaulted)
        {
            return GeneralErrors.ValueNotValid("locations").ToErrors();
        }

        var resTranz = await _connectionManeger.CreateTranzit(cancellationToken);

        if (resTranz.IsFailure)
        {
            return resTranz.Error;
        }

        await using var tranzit = resTranz.Value;

        if (command.dto.locationsId.Length <= 0)
        {
            return GeneralErrors.ValueNotValid("logic").ToErrors();
        }

        List<LocationId> locId = command.dto.locationsId.Select(q => LocationId.Create(q)).ToList();

        if (!_locationRepository.ChekActivetiLocations(locId, cancellationToken).Result.Value)
        {
            return GeneralErrors.ValueNotValid("logic").ToErrors();
        }

        DepartamentId departamentId = DepartamentId.Create(command.departamentId);

        await _departamentRepository.UpdateLocations(locId, departamentId, cancellationToken);

        var dept = await _departamentRepository.GetDepartamentById(DepartamentId.Create(command.departamentId), cancellationToken);
        if (dept.IsFailure)
        {
            _logger.LogInformation("не найден такой департмаент");
            return GeneralErrors.Update("SoftDeleteDept").ToErrors();
        }

        await tranzit.PublishToOutboxAsync(new DepartmentLocationsUpdated(dept.Value.Id.ValueId, dept.Value.Name.ValueName, command.dto.locationsId));

        await tranzit.Commit(cancellationToken);

        return departamentId.ValueId;
    }
}