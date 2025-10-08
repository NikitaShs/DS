using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Contract;
using PetDS.Domain.Location;
using PetDS.Domain.Location.VO;
using PetDS.Domain.Shered;

namespace PetDS.Application.Locations.CreateLocation
{
    public class LocationCreateService : IHandler<Guid, CreateLocationCommand>
    {
        private readonly IValidator<CreateLocationDto> _validator;
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<LocationCreateService> _logger;

        public LocationCreateService(
            ILocationRepository locationRepository,
            ILogger<LocationCreateService> logger,
            IValidator<CreateLocationDto> validator)
        {
            _locationRepository = locationRepository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid, Error>> Handel(
            CreateLocationCommand createLocation,
            CancellationToken cancellationToken)
        {
            var dto = createLocation.dto;

            var resultDto = await _validator.ValidateAsync(dto);

            if (!resultDto.IsValid)
            {
                return GeneralErrors.ValueNotValid("reqvest");
            }

            var name = LocationName.Create(dto.name);
            if (name.IsFailure)
            {
                return GeneralErrors.ValueFailure("name");
            }

            var loc = Location.Create(name.Value, dto.city, dto.region, dto.strit, dto.namberHouse);
            if (loc.IsFailure)
            {
                return GeneralErrors.ValueFailure("location");
            }

            _logger.LogInformation("Location создана");
            await _locationRepository.AddLocation(loc.Value, cancellationToken);

            return loc.Value.Id.ValueId;
        }
    }
}
