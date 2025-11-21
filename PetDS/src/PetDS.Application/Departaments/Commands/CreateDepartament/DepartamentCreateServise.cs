using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Application.Locations;
using PetDS.Contract;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location.VO;
using PetDS.Domain.Shered;

namespace PetDS.Application.Departaments.CreateDepartament;

public class DepartamentCreateServise : IHandler<Guid, CreateDepartamentCommand>
{
    private readonly IDepartamentRepository _departamentRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly ILogger<DepartamentCreateServise> _logger;
    private readonly IValidator<CreateDepartamentDto> _validator;

    public DepartamentCreateServise(IDepartamentRepository departamentRepository,
        ILogger<DepartamentCreateServise> logger,
        IValidator<CreateDepartamentDto> validator, ILocationRepository locationRepository)
    {
        _departamentRepository = departamentRepository;
        _logger = logger;
        _validator = validator;
        _locationRepository = locationRepository;
    }

    public async Task<Result<Guid, Errors>> Handler(CreateDepartamentCommand command,
        CancellationToken cancellationToken = default)
    {
        var dto = command.departamentDto;

        var idLoc = dto.locationsId.Select(q => LocationId.Create(q)).ToList();

        if (!_locationRepository.ChekAvailabilityIdLocation(idLoc, cancellationToken).Result.Value)
        {
            return GeneralErrors.ValueNotValid("logic").ToErrors();
        }

        ValidationResult? result = _validator.Validate(dto);

        if (!result.IsValid)
        {
            return GeneralErrors.ValueNotValid("dto").ToErrors();
        }

        Result<DepartamentName, Error> name = DepartamentName.Create(dto.name);

        if (name.IsFailure)
        {
            _logger.LogInformation("name не создан");
            return GeneralErrors.ValueFailure("name").ToErrors();
        }

        Result<DepartamentIdentifier, Error> identifier = DepartamentIdentifier.Create(dto.identifier);
        if (identifier.IsFailure)
        {
            _logger.LogInformation("identifier не создан");
            return GeneralErrors.ValueFailure("identifier").ToErrors();
        }

        var locIds = dto.locationsId.Select(id => LocationId.Create(id)).ToList();
        Result<Departament, Error> departament = dto.parantId is null
            ? Departament.Create(name.Value, identifier.Value, null, locIds)
            : Departament.Create(name.Value, identifier.Value,
                _departamentRepository.GetDepartamentById(DepartamentId.Create(dto.parantId.Value), cancellationToken)
                    .Result.Value, locIds);

        if (departament.IsFailure)
        {
            _logger.LogInformation("Departament не создан");
            return GeneralErrors.ValueFailure("Departament").ToErrors();
        }

        await _departamentRepository.AddDepartament(departament.Value, cancellationToken);

        return departament.Value.Id.ValueId;
    }
}