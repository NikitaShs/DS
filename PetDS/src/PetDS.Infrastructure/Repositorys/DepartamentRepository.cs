using CSharpFunctionalExtensions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Application.Departaments;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location.VO;
using PetDS.Domain.Shered;
using PetDS.Infrastructure.DataBaseConnections;
using System.Data.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PetDS.Infrastructure.Repositorys;

public class DepartamentRepository : IDepartamentRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IConnectionFactory _connectionFactory;
    private readonly ILogger<DepartamentRepository> _logger;
    private readonly HybridCache _hybridCache;

    public DepartamentRepository(ApplicationDbContext applicationDbContext, ILogger<DepartamentRepository> logger,
        IConnectionFactory connectionFactory, HybridCache hybridCache)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
        _connectionFactory = connectionFactory;
        _hybridCache = hybridCache;
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
            if (departament.Parent != null)
            {
                _hybridCache.RemoveByTagAsync(CacheTags.DepartamentChilds);
                _hybridCache.RemoveByTagAsync(CacheTags.DepartamentTopPosition);
            }
            else
                _hybridCache.RemoveByTagAsync(CacheTags.Departament);
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

        _hybridCache.RemoveByTagAsync(CacheTags.Departament);

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
                UPDATE departaments dep SET
                    parent_id = p.id,
                    path = (p.path::text || '.' || dep.name)::ltree,
                    depth = nlevel((p.path::text || '.' || dep.name)::ltree)
                    FROM Parent p
                    WHERE dep.id = @id
                    
                    RETURNING dep.id, dep.path, dep.depth, dep.name
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

        if (res >= 0)
        {
            var updated = await _applicationDbContext.Departaments
                .Where(d => d.Id == DepartamentId.Create(id))
                .Select(d => d.ParentId)
                .FirstOrDefaultAsync(cancellationToken);

            if (!parentId.HasValue)
            {
                return updated == null ? 1 : GeneralErrors.Update("Hierarchy").ToErrors();
            }
            else
            {
                return updated.ValueId == parentId.Value ? 1 : GeneralErrors.Update("Hierarchy").ToErrors();
            }
        }


        return GeneralErrors.Update("Hierarchy").ToErrors();



    }

    public async Task<Result<bool, Errors>> CheckingDepartamentExistence(DepartamentId departamentId, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Departaments.AnyAsync(q => q.Id == departamentId);
    }

    public async Task<Result<bool, Errors>> SoftDeleteDept(Guid departamentId, CancellationToken cancellationToken)
    {
        var conn = _applicationDbContext.Database.GetDbConnection();

        string sql = """
                     WITH OldDept AS (
                                     UPDATE departaments
                                      SET path = (subpath(path, 0, -1) || ('delete-' || name))::ltree,
                                     is_active = false,
                                          deleted_at = @timeDel
                                      WHERE id = @Depid
                                     RETURNING departaments.id, departaments.path, departaments.depth, departaments.name
                         )
                     
                     
                     UPDATE departaments dep SET
                                             path = (OldDept.path || subpath(dep.path, OldDept.depth))
                                             FROM OldDept
                                             WHERE OldDept.id != dep.id AND dep.path <@ (subpath(OldDept.path, 0, -1) || OldDept.name)::ltree;
                     """;
        var delTime = DateTime.UtcNow.AddYears(1);

        _logger.LogInformation(delTime.ToString());

        var res = await conn.ExecuteAsync(sql, new { Depid = departamentId, timeDel = delTime});


        if (res > 0)
            return true;
        else
            return GeneralErrors.Update("SoftDeleteDept").ToErrors();
    }
}