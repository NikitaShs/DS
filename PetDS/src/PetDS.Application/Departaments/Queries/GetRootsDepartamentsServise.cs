using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Contract.Departamen.Queries;
using PetDS.Domain.Departament;
using PetDS.Domain.Shered;
using SharedKernel.Exseption;

namespace PetDS.Application.Departaments.Queries;

public class GetRootsDepartamentsServise
{
    private readonly HybridCache _cache;
    private readonly ILogger<GetRootsDepartamentsServise> _logger;
    private readonly IReadDbContext _readDbContext;

    public GetRootsDepartamentsServise(ILogger<GetRootsDepartamentsServise> logger, IReadDbContext readDbContext,
        HybridCache cache)
    {
        _logger = logger;
        _readDbContext = readDbContext;
        _cache = cache;
    }

    public async Task<Result<DepartamenthAndChildDto, Errors>> Handler(RootsDepartementReqvestDto reqvestDto,
        CancellationToken cancellationToken) =>
        await _cache.GetOrCreateAsync(
            GeneralKeyCache.KeyDeptRootsPagination(reqvestDto.Page, reqvestDto.SizePage, reqvestDto.prefetch),
            async _ => await DeptGet(reqvestDto),
            new HybridCacheEntryOptions
            {
                LocalCacheExpiration = TimeSpan.FromMinutes(10), Expiration = TimeSpan.FromMinutes(5)
            }, [CacheTags.Departament, CacheTags.DepartamentChilds, CacheTags.DepartamentRoots], cancellationToken);

    private async Task<DepartamenthAndChildDto> DeptGet(RootsDepartementReqvestDto reqvestDto)
    {
        IQueryable<Departament> req = _readDbContext.ReadDepartament;

        req = req.Where(q => q.ParentId == null).Include(q => q.Children);

        long totalCount = await req.LongCountAsync();

        req = req.OrderBy(q => q.CreateAt).Take(reqvestDto.SizePage)
            .Skip((reqvestDto.Page - 1) * reqvestDto.SizePage);

        List<DepartamenthAndChildModel> res = await req.Select(q => new DepartamenthAndChildModel
        {
            Id = q.Id.ValueId,
            Name = q.Name.ValueName,
            Identifier = q.Identifier.ValueIdentifier,
            ParentId = q.ParentId.ValueId,
            Depth = q.Depth,
            Path = q.Path.ValuePash,
            IsActive = q.IsActive,
            HasMoreChildren = q.Children.Count > 0,
            Childs = q.Children.Take(reqvestDto.prefetch).Select(Cq => new DepartamenthModelClear
            {
                Id = Cq.Id.ValueId,
                Name = Cq.Name.ValueName,
                Identifier = Cq.Identifier.ValueIdentifier,
                ParentId = Cq.ParentId.ValueId,
                Depth = Cq.Depth,
                Path = Cq.Path.ValuePash,
                IsActive = Cq.IsActive,
                HasMoreChildren = Cq.Children.Count > 0
            }).ToList()
        }).ToListAsync();

        if (!res.Any())
        {
            return null;
        }

        return new DepartamenthAndChildDto(res, totalCount);
    }
}