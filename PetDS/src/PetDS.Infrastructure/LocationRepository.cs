using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<LocationRepository> _logger;

        public LocationRepository(ApplicationDbContext dbContext, ILogger<LocationRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> AddLocation(Location location, CancellationToken cancellationToken)
        {
            await _dbContext.Locations.AddAsync(location, cancellationToken);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Location сохранена в базу данных");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Location не сохранена в базу данных");
                return Error.Unknown("necto", "не известная ошибка");
            }

            return location.Id.ValueId;
        }
    }
}
