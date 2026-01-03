using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Contract.Departamen.Queries;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Shered;

namespace PetDS.Application.Departaments.Queries
{
    public class GetChildsDepartamentsServise
    {
        private readonly ILogger<GetChildsDepartamentsServise> _logger;

        private readonly IReadDbContext _readDbContext;

        private readonly HybridCache _cache;

        public GetChildsDepartamentsServise(ILogger<GetChildsDepartamentsServise> logger, IReadDbContext readDbContext, HybridCache cache)
        {
            _logger = logger;
            _readDbContext = readDbContext;
            _cache = cache;
        }

        public async Task<Result<List<DepartamenthModelClear>, Errors>> Handler(Guid parentId, DepartamentPaginationReqvestDto reqvestDto, CancellationToken cancellationToken)
        {

            return await _cache.GetOrCreateAsync(
                $"DeptChilds_parentId{parentId}_page{reqvestDto.Page}_pageSize{reqvestDto.SizePage}",
                async _ =>
                {
                    var res = await _readDbContext.ReadDepartament
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
                        HasMoreChildren = q.Children.Count > 0,
                    }).ToListAsync(cancellationToken);
                    if (!res.Any())
                        return [];
                    return res;
                }, new HybridCacheEntryOptions {
                    LocalCacheExpiration = TimeSpan.FromMinutes(10),
                    Expiration = TimeSpan.FromMinutes(5)
                }, new List<string> { CacheTags.Departament, CacheTags.DepartamentChilds }, cancellationToken);
        }
    }
}
