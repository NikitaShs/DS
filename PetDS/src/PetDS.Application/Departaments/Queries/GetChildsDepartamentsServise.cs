using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Contract.Departamen.Queries;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Shered;
using SharedKernel.Exseption;

namespace PetDS.Application.Departaments.Queries;

public class GetChildsDepartamentsServise
{
    private readonly HybridCache _cache;
    private readonly ILogger<GetChildsDepartamentsServise> _logger;

    private readonly IReadDbContext _readDbContext;

    public GetChildsDepartamentsServise(ILogger<GetChildsDepartamentsServise> logger, IReadDbContext readDbContext,
        HybridCache cache)
    {
        _logger = logger;
        _readDbContext = readDbContext;
        _cache = cache;
    }

    public async Task<Result<List<DepartamenthModelClear>, Errors>> Handler(Guid parentId,
        DepartamentPaginationReqvestDto reqvestDto, CancellationToken cancellationToken) =>
        await _cache.GetOrCreateAsync(
            GeneralKeyCache.KeyDeptChildPagination(parentId, reqvestDto.Page, reqvestDto.SizePage),
            async _ =>
            {
                List<DepartamenthModelClear> res = await _readDbContext.ReadDepartament
                    .Where(q => q.ParentId == DepartamentId.Create(parentId)).Include(q => q.Children)
                    .OrderBy(q => q.CreateAt).Take(reqvestDto.SizePage)
                    .Skip((reqvestDto.Page - 1) * reqvestDto.SizePage)
                    .Select(q => new DepartamenthModelClear
                    {
                        Id = q.Id.ValueId,
                        Name = q.Name.ValueName,
                        Identifier = q.Identifier.ValueIdentifier,
                        ParentId = q.ParentId.ValueId,
                        Depth = q.Depth,
                        Path = q.Path.ValuePash,
                        IsActive = q.IsActive,
                        HasMoreChildren = q.Children.Count > 0
                    }).ToListAsync(cancellationToken);
                if (!res.Any())
                {
                    return [];
                }

                return res;
            },
            new HybridCacheEntryOptions
            {
                LocalCacheExpiration = TimeSpan.FromMinutes(10), Expiration = TimeSpan.FromMinutes(5)
            }, [CacheTags.Departament, CacheTags.DepartamentChilds], cancellationToken);
}