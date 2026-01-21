using System.Data.Common;
using Core.Adstract;
using CSharpFunctionalExtensions;
using Dapper;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using PetDS.Contract.Departamen.Queries;
using PetDS.Domain.Shered;
using SharedKernel.Exseption;

namespace PetDS.Application.Departaments.Queries;

public class GetTopFiveDepartamentsServise
{
    private readonly HybridCache _cache;
    private readonly IConnectionFactory _connectionFactory;

    private ILogger<GetTopFiveDepartamentsServise> _logger;

    public GetTopFiveDepartamentsServise(IConnectionFactory connectionFactory,
        ILogger<GetTopFiveDepartamentsServise> logger, HybridCache cache)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
        _cache = cache;
    }

    public async Task<Result<List<DepartamentModelDto>, Errors>> Handler(CancellationToken cancellationToken)
    {
        using DbConnection connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);

        return await _cache.GetOrCreateAsync(GeneralKeyCache.KeyDeptTops("position", 5), async _ =>
            {
                IEnumerable<DepartamentModelDto> res = await connection.QueryAsync<DepartamentModelDto>("""
                    SELECT
                        dep.name,
                        dep.identifier,
                        dep.parent_id,
                        dep.depth,
                        dep.is_active,
                        dep.path,
                        COUNT(dep.id) as countPos
                    FROM departaments dep
                            JOIN "departamentPositions" dp ON dep.id = dp.departament_id
                    GROUP BY
                        dep.id
                     ORDER BY countPos DESC LIMIT 5;
                    """);
                if (!res.Any())
                {
                    return [];
                }

                return res.ToList();
            },
            new HybridCacheEntryOptions
            {
                LocalCacheExpiration = TimeSpan.FromMinutes(10), Expiration = TimeSpan.FromMinutes(5)
            }, [CacheTags.Departament, CacheTags.DepartamentTopPosition], cancellationToken);
    }
}