using CSharpFunctionalExtensions;
using PetDS.Domain.Location;
using PetDS.Domain.Location.VO;
using SharedKernel.Exseption;

namespace PetDS.Application.Locations;

public interface ILocationRepository
{
    Task<Result<Guid, Error>> AddLocation(Location location, CancellationToken cancellationToken = default);

    Task<Result<bool, Errors>> ChekAvailabilityIdLocation(List<LocationId> locationIds,
        CancellationToken cancellationToken);

    Task<Result<bool, Errors>> ChekActivetiLocations(List<LocationId> locationIds, CancellationToken cancellationToken);
}