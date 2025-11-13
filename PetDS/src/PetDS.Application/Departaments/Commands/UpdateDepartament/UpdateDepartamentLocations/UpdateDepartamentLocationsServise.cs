using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Application.Locations;
using PetDS.Contract.Departamen;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location.VO;
using PetDS.Domain.Shered;

namespace PetDS.Application.Departaments.Commands.UpdateDepartament.UpdateDepartamentLocations;

public class UpdateDepartamentLocationsServise : IHandler<Guid, UpdateDepartamentLocationsCommand>
{
    private readonly IDepartamentRepository _departamentRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly ILogger<UpdateDepartamentLocationsServise> _logger;
    private readonly IValidator<UpdateDepartamentLocationsDto> _validator;

    public UpdateDepartamentLocationsServise(
        IValidator<UpdateDepartamentLocationsDto> validator,
        ILogger<UpdateDepartamentLocationsServise> logger,
        IDepartamentRepository departamentRepository,
        ILocationRepository locationRepository)
    {
        _validator = validator;
        _logger = logger;
        _departamentRepository = departamentRepository;
        _locationRepository = locationRepository;
    }

    public async Task<Result<Guid, Errors>> Handler(UpdateDepartamentLocationsCommand command,
        CancellationToken cancellationToken = default)
    {
        Task<ValidationResult>? result = _validator.ValidateAsync(command.dto, cancellationToken);
        if (result.IsFaulted)
        {
            return GeneralErrors.ValueNotValid("locations").ToErrors();
        }

        var locId = command.dto.locationsId.Select(q => LocationId.Create(q)).ToList();

        if (!_locationRepository.ChekActivetiLocations(locId, cancellationToken).Result.Value)
        {
            return GeneralErrors.ValueNotValid("logic").ToErrors();
        }

        DepartamentId departamentId = DepartamentId.Create(command.departamentId);

        await _departamentRepository.UpdateLocations(locId, departamentId, cancellationToken);

        return departamentId.ValueId;
    }
}