using CSharpFunctionalExtensions;
using PetDS.Application.abcstractions;
using PetDS.Domain.Location;
using PetDS.Domain.Location.VO;
using PetDS.Domain.Shered;
using System;
using System.IO;


namespace PetDS.Application.Locations.CreateLocation
{
    public class LocationCreateService : IHandler<Guid, CreateLocationCommand>
    {
        private readonly ILocationRepository _locationRepository;

        public LocationCreateService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Result<Guid, Error>> Handel(
            CreateLocationCommand createLocation,
            CancellationToken cancellationToken)
        {
            var dto = createLocation.dto;
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

            await _locationRepository.AddLocation(loc.Value, cancellationToken);

            return loc.Value.Id.ValueId;
        }
    }
}
