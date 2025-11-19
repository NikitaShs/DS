using PetDS.Application.abcstractions;
using PetDS.Contract.Departamen.Queries;
using PetDS.Domain.Departament.VO;
using Microsoft.EntityFrameworkCore;


namespace PetDS.Application.Departaments.Queries
{
    public class GetByIdDepartament
    {
        //private readonly IReadDbContext _dbContext;

        //public GetByIdDepartament(IReadDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        //public async Task<DepartamentModelDto?> Hendler(DepartamentByIDDto dto, CancellationToken cancellationToken)
        //{
        //    return await _dbContext.ReadDepartament.Where(q => q.Id == DepartamentId.Create(dto.departamentId))
        //        .Select(q => new DepartamentModelDto
        //        {
        //            Id = q.Id.ValueId,
        //            Name = q.Name.ValueName,
        //            Identifier = q.Identifier.ValueIdentifier,
        //            ParentId = q.ParentId.ValueId,
        //            Path = q.Path.ValuePash,
        //            Depth = q.Depth,

        //        }
        //        ).FirstOrDefaultAsync(cancellationToken);
        //}
    }
}
