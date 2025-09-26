using CSharpFunctionalExtensions;
using PetDS.Contract;
using PetDS.Domain.Location;
using PetDS.Domain.Location.VO;

namespace PetDS.Application
{
    public class LocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Result<Guid>> CreateLoc(
            CreateLocationRequest createLocation,
            CancellationToken cancellationToken)

        {
            var name = LocationName.Create(createLocation.name).Value;

            var loc = Location.Create(name, createLocation.city, createLocation.region, createLocation.strit, createLocation.namberHouse).Value;

            await _locationRepository.AddLocation(loc, cancellationToken);

            return Result.Success(loc.Id.ValueId);
        }
    }
}
