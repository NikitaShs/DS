using System.Data.Common;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Application.Departaments;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location.VO;
using PetDS.Domain.Shered;
using PetDS.Infrastructure.DataBaseConnections;

namespace PetDS.Infrastructure.Repositorys;

public class DepartamentRepository : IDepartamentRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IConnectionFactory _connectionFactory;
    private readonly ILogger<DepartamentRepository> _logger;

    public DepartamentRepository(ApplicationDbContext applicationDbContext, ILogger<DepartamentRepository> logger,
        IConnectionFactory connectionFactory)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<Guid, Errors>> AddDepartament(Departament departament,
        CancellationToken cancellationToken)
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

    public async Task<Result<Departament, Errors>> GetDepartamentById(DepartamentId id,
        CancellationToken cancellationToken)
    {
        Departament? result =
            await _applicationDbContext.Departaments.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);

        if (result == null)
        {
            _logger.LogInformation("депортамент по id: {id}", id);
            return Result.Failure<Departament, Errors>(GeneralErrors.Unknown().ToErrors());
        }

        return result;
    }

    public async Task<Result<List<Departament>, Errors>> GetDepartamentsById(List<DepartamentId> ids,
        CancellationToken cancellationToken)
    {
        List<Departament> result = await _applicationDbContext.Departaments
            .Where(q => ids.Contains(q.Id) && q.IsActive == true)
            .ToListAsync(cancellationToken);

        if (result.Count != ids.Count)
        {
            _logger.LogInformation("не все DepartamentId существуют или активны");
            return GeneralErrors.Unknown().ToErrors();
        }

        return result;
    }


    public async Task<Result<Guid, Errors>> UpdateLocations(
        List<LocationId> locationIds,
        DepartamentId departamentId,
        CancellationToken cancellationToken)
    {
        foreach (LocationId i in locationIds)
        {
            await _applicationDbContext.DepartamentLocations.Where(q => q.DepartamentId == departamentId)
                .ExecuteUpdateAsync(
                    q => q.SetProperty(v => v.LocationId, i), cancellationToken);
        }

        return departamentId.ValueId;
    }

    public async Task<Result<Departament, Errors>> GetDepartamentFullHierahiById(DepartamentId id,
        CancellationToken cancellationToken)
    {
        const string selDap = """
                              WITH RECURSIVE tree_dept As(
                                  SELECT d.* 
                                  FROM departaments d
                                             WHERE d.id = '719f7651-eb9d-46c8-a780-f71203b6c873'
                                             UNION ALL 
                                             SELECT c.*
                                             FROM departaments c
                                             JOIN tree_dept dt ON c.parent_id = dt.id)
                              SELECT id,
                                     parent_id,
                                     name_value_name,
                                     identifier_value_identifier,
                                     path_value_pash,
                                     depth,
                                     is_active,
                                     create_at,
                                     update_at
                                  FROM tree_dept
                              """;
        DbConnection con = _applicationDbContext.Database.GetDbConnection();

        var dep = await con.Q
        var ee = res;
        return res;
    }
}