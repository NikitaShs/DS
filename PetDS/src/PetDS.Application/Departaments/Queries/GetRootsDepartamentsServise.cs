using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
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

        public GetRootsDepartamentsServise(ILogger<GetRootsDepartamentsServise> logger, IReadDbContext readDbContext)
        {
            _logger = logger;
            _readDbContext = readDbContext;
        }

        public async Task<Result<List<DepartamenthAndChildModel>, Errors>> Handler(RootsDepartementReqvestDto reqvestDto, CancellationToken cancellationToken)
        {
            var req = _readDbContext.ReadDepartament
                .Where(q => q.ParentId == null).Include(q => q.Children)
                .OrderBy(q => q.CreateAt).Take(reqvestDto.SizePage)
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
                Childs = q.Children.Take(reqvestDto.prefetch).Select(Cq => new DepartamenthModelClear
                {
                    Id = Cq.Id.ValueId,
                    Name = Cq.Name.ValueName,
                    Identifier = Cq.Identifier.ValueIdentifier,
                    ParentId = Cq.ParentId.ValueId,
                    Depth = Cq.Depth,
                    Path = Cq.Path.ValuePash,
                    IsActive = Cq.IsActive
                }).ToList(),
            }).ToListAsync();


            return res;
        }
    }
}
