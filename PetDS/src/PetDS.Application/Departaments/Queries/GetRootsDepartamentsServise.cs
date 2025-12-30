using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Contract.Departamen.Queries;
using PetDS.Domain.Shered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application.Departaments.Queries
{
    public class GetRootsDepartamentsServise
    {
        private readonly ILogger<GetRootsDepartamentsServise> _logger;
        private readonly IReadDbContext _readDbContext;
        private readonly HybridCache _cache;

        public GetRootsDepartamentsServise(ILogger<GetRootsDepartamentsServise> logger, IReadDbContext readDbContext, HybridCache cache)
        {
            _logger = logger;
            _readDbContext = readDbContext;
            _cache = cache;
        }

        public async Task<Result<DepartamenthAndChildDto, Errors>> Handler(RootsDepartementReqvestDto reqvestDto, CancellationToken cancellationToken)
        {

            return await _cache.GetOrCreateAsync(
                $"Rootdepartament_page{reqvestDto.Page}_sizePage{reqvestDto.SizePage}_prefetch{reqvestDto.prefetch}",
                async _ => await DeptGet(reqvestDto));

        }

        private async Task<DepartamenthAndChildDto> DeptGet(RootsDepartementReqvestDto reqvestDto)
        {
            var req = _readDbContext.ReadDepartament;

            req = req.Where(q => q.ParentId == null).Include(q => q.Children);

            var totalCount = await req.LongCountAsync();

            req = req.OrderBy(q => q.CreateAt).Take(reqvestDto.SizePage)
                .Skip((reqvestDto.Page - 1) * reqvestDto.SizePage);

            var res = await req.Select(q => new DepartamenthAndChildModel
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
                }).ToList(),
            }).ToListAsync();

            if (!res.Any())
                return null;

            return new DepartamenthAndChildDto(res, totalCount);
        }
    }
}
