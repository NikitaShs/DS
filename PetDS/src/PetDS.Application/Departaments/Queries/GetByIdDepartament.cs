using PetDS.Application.abcstractions;
using PetDS.Contract.Departamen.Queries;
using PetDS.Domain.Departament.VO;
using Microsoft.EntityFrameworkCore;


namespace PetDS.Application.Departaments.Queries
{
    public class GetByIdDepartament
    {
        private readonly IReadDbContext _dbContext;

        public GetByIdDepartament(IReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
