using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Contract.Departamen.Queries;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Shered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application.Departaments.Queries
{
    public class GetChildsDepartamentsServise
    {
        private readonly ILogger<GetChildsDepartamentsServise> _logger;

        private readonly IReadDbContext _readDbContext;

        public GetChildsDepartamentsServise(ILogger<GetChildsDepartamentsServise> logger, IReadDbContext readDbContext)
        {
            _logger = logger;
            _readDbContext = readDbContext;
        }

        public async Task<Result<List<DepartamenthModelClear>, Errors>> Handler(Guid parentId, DepartamentPaginationReqvestDto reqvestDto, CancellationToken cancellationToken)
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
                }).ToListAsync();

            return res;
        }
    }
}
