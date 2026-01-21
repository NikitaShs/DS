using Core.Adstract;
using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using PetDS.Application.Locations.Commands.CreateLocation;
using PetDS.Contract;
using PetDS.Domain.Location;
using PetDS.Domain.Location.VO;
using SharedKernel.Exseption;

namespace PetDS.Application.Locations.CreateLocation;

public class LocationCreateService : IHandler<Guid, CreateLocationCommand>
{
    private readonly ILocationRepository _locationRepository;
    private readonly ILogger<LocationCreateService> _logger;
    private readonly IValidator<CreateLocationDto> _validator;

    public LocationCreateService(
        ILocationRepository locationRepository,
        ILogger<LocationCreateService> logger,
        IValidator<CreateLocationDto> validator)
    {
        _locationRepository = locationRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, Errors>> Handler(
        CreateLocationCommand createLocation,
        CancellationToken cancellationToken)
    {
        CreateLocationDto dto = createLocation.dto;

        ValidationResult? resultDto = await _validator.ValidateAsync(dto);

        if (!resultDto.IsValid)
        {
            return GeneralErrors.ValueNotValid("reqvest").ToErrors();
        }

        Result<LocationName, Error> name = LocationName.Create(dto.name);
        if (name.IsFailure)
        {
            return GeneralErrors.ValueFailure("name").ToErrors();
        }

        Result<Location, Error> loc = Location.Create(name.Value, dto.city, dto.region, dto.strit, dto.namberHouse);
        if (loc.IsFailure)
        {
            return GeneralErrors.ValueFailure("location").ToErrors();
        }

        _logger.LogInformation("Location создана");
        await _locationRepository.AddLocation(loc.Value, cancellationToken);

        return loc.Value.Id.ValueId;
    }
}