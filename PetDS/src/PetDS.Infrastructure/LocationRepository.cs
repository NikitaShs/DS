using CSharpFunctionalExtensions;
using PetDS.Application.Locations;
using PetDS.Domain.Location;
using PetDS.Domain.Shered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Infrastructure
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LocationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Guid, Error>> AddLocation(Location location, CancellationToken cancellationToken)
        {
            await _dbContext.Locations.AddAsync(location, cancellationToken);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Error.Unknown("necto", "не известная ошибка");
            }

            return location.Id.ValueId;
        }
    }
}
