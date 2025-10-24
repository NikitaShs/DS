using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetDS.Application.Positions;
using PetDS.Domain.Departament;
using PetDS.Domain.Position;
using PetDS.Domain.Shered;
using PetDS.Infrastructure.DataBaseConnections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Infrastructure.Repositorys
{
    public class PositionRepository : IPositionRepositiry
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<PositionRepository> _logger;

        public PositionRepository(ApplicationDbContext dbContext, ILogger<PositionRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Result<Guid, Errors>> AddPosition(Position position, CancellationToken cancellationToken)
        {
            await _dbContext.Positions.AddAsync(position);
            _logger.LogInformation("позиця отслеживаеться");

            try
            {
                _dbContext.SaveChanges();
                _logger.LogInformation("позиця созранена в БД");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "позиця не созранена в БД");
            }

            return position.Id.ValueId;
        }
    }
}
