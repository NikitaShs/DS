using CSharpFunctionalExtensions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Application.Departaments;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location.VO;
using PetDS.Domain.Shered;
using PetDS.Infrastructure.DataBaseConnections;
using System.Data.Common;

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

    public async Task<Result<int, Errors>> UpdateDepartamentFullHierahiById(Guid id, Guid? parentId,
        CancellationToken cancellationToken)
    {

        var connection = _applicationDbContext.Database.GetDbConnection();

        var sql = string.Empty;

        if (parentId != null)
        {
            sql = """
            
            WITH Parent AS
                     (
                         SELECT id, path FROM departaments
                         WHERE id = @parentId
                            FOR UPDATE
                     ),
                 OldDept AS
                     (
                         SELECT id, depth, path, name FROM departaments
                         WHERE id = @id
                            FOR UPDATE
                     ),
                New_Dept AS
            (
                UPDATE departaments SET 
                    parent_id = p.id,
                    path = (p.path::text || '.' || departaments.name)::ltree,
                    depth = nlevel((p.path::text || '.' || departaments.name)::ltree)
                    FROM Parent p
                    WHERE departaments.id = @id
                    
                    RETURNING departaments.id, departaments.path, departaments.depth, departaments.name
            )
            UPDATE departaments SET 
                                    path = (New_Dept.path::text || subpath(departaments.path, OldDept.depth))::ltree,
                                    depth = nlevel((New_Dept.path::text || subpath(departaments.path, OldDept.depth))::ltree)
                                
                                    FROM New_Dept, OldDept
                                    WHERE departaments.path <@ OldDept.path AND New_Dept.id != departaments.id
            
            """;
        }
        else
        {
            sql = """
            
            WITH OldDept AS
                     (
                         SELECT id, depth, path, name FROM departaments
                         WHERE id = @id
                            FOR UPDATE
                     ),
                New_Dept AS
            (
                UPDATE departaments SET 
                    parent_id = null,
                    path = departaments.name::ltree,
                    depth = 1
                    WHERE departaments.id = @id
                    
                    RETURNING departaments.id, departaments.path, departaments.depth, departaments.name
            )
            UPDATE departaments SET 
                                    path = (New_Dept.path::text || subpath(departaments.path, OldDept.depth))::ltree,
                                    depth = nlevel((New_Dept.path::text || subpath(departaments.path, OldDept.depth))::ltree)
                                
                                    FROM New_Dept, OldDept
                                    WHERE departaments.path <@ OldDept.path AND New_Dept.id != departaments.id
            
            """;
        }

        var res = await connection.ExecuteAsync(sql, new { parentId = parentId, id = id });

        _logger.LogInformation($"затронуто {res}");
        
        return res;

    }

    public async Task<Result<bool, Errors>> CheckingDepartamentExistence(DepartamentId departamentId, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Departaments.AnyAsync(q => q.Id == departamentId);
    }
}