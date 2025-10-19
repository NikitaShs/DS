using CSharpFunctionalExtensions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetDS.Application.Departaments;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location.VO;
using PetDS.Domain.Shered;
using PetDS.Infrastructure.DataBaseConnections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Infrastructure.Repositorys
{
    public class DepartamentRepository : IDepartamentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<DepartamentRepository> _logger;
        private readonly IConnectionFactory _connectionFactory;

        public DepartamentRepository(ApplicationDbContext applicationDbContext, ILogger<DepartamentRepository> logger, IConnectionFactory connectionFactory)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
            _connectionFactory = connectionFactory;
        }

        public async Task<Result<Guid, Errors>> AddDepartament(Departament departament, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Departament отслеживаеться");
            await _applicationDbContext.Departaments.AddAsync(departament, cancellationToken);

            try
            {
                _applicationDbContext.SaveChanges();
                _logger.LogInformation("Departament сохранён");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Departament не сохранён");
                return GeneralErrors.ValueFailure("Departament").ToErrors();
            }

            return departament.Id.ValueId;
        }

        public async Task<Result<Departament, Errors>> GetDepartamentById(DepartamentId id, CancellationToken cancellationToken)
        {
            var result = await _applicationDbContext.Departaments.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);

            if(result == null)
            {
                _logger.LogInformation("депортамент по id: {id}", id);
                return Result.Failure<Departament, Errors>(GeneralErrors.Unknown().ToErrors());
            }

            return result;
        }

        public async Task<Result<List<Departament>, Errors>> GetDepartamentsById(List<DepartamentId> ids, CancellationToken cancellationToken)
        {
            var result = await _applicationDbContext.Departaments.Where(q => ids.Contains(q.Id) && q.IsActive == true).ToListAsync(cancellationToken);

            if (result.Count != ids.Count)
            {
                _logger.LogInformation("не все DepartamentId существуют или активны");
                return GeneralErrors.Unknown().ToErrors();
            }

            return result;
        }

        public async Task<Result<Guid, Errors>> UpdateLocations(List<LocationId> locationIds, DepartamentId departamentId, CancellationToken cancellationToken)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);

            using var transaction = connection.BeginTransaction();

            try
            {

                const string updateNameSql =
                    """
                    SELECT * 
                    FROM Users 
                    WHERE Id = @Id

                    """;
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
