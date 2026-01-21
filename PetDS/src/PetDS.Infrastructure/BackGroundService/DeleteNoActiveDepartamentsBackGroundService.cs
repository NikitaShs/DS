using System.Data.Common;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetDS.Infrastructure.DataBaseConnections;

namespace PetDS.Infrastructure.BackGroundService;

public class DeleteNoActiveDepartamentsBackGroundService : BackgroundService
{
    private readonly ILogger<DeleteNoActiveDepartamentsBackGroundService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public DeleteNoActiveDepartamentsBackGroundService(ILogger<DeleteNoActiveDepartamentsBackGroundService> logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using IServiceScope scope = _scopeFactory.CreateScope();

        ApplicationDbContext dbContect = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        PeriodicTimer timer = new(TimeSpan.FromDays(1));

        DbConnection connection = dbContect.Database.GetDbConnection();

        string sql = """
                     WITH delete_dept AS (
                         DELETE FROM departaments
                             WHERE is_active = false AND deleted_at <= @newTime
                             RETURNING id, parent_id, path, depth
                                ),
                         delete_join_pos AS(
                           DELETE FROM "departamentPositions"
                               USING delete_dept
                               WHERE departament_id = delete_dept.id
                         ),
                          delete_join_loc AS(
                              DELETE FROM "departamentLocations"
                                  USING delete_dept
                                  WHERE departament_id = delete_dept.id
                          ),
                             update_clild AS (

                                 UPDATE departaments Cdep
                                     SET
                                         parent_id = delete_dept.parent_id,
                                         path = (subpath(delete_dept.path, 0, - 1) || Cdep.name::ltree),
                                         depth = Cdep.depth - 1 
                                     FROM delete_dept
                                     WHERE Cdep.parent_id = delete_dept.id
                                        RETURNING Cdep.id, Cdep.path
                                        )
                     UPDATE departaments del
                             SET
                                 path = (subpath(update_clild.path, 0, -1) || subpath(del.path, delete_dept.depth)),
                                 depth = del.depth - 1
                         FROM update_clild, delete_dept
                         WHERE del.path <@ delete_dept.path ANd del.id != delete_dept.id AND del.id != update_clild.id
                     """;

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            int res = await connection.ExecuteAsync(sql, new { newTime = DateTime.UtcNow });
            _logger.LogInformation(")");
            if (res > 0)
            {
                _logger.LogInformation("опа удаление");
            }
        }
    }
}