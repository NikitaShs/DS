using PetDS.Application.abcstractions;

namespace PetDS.Application.Departaments.Queries;

public class GetByIdDepartament
{
    private readonly IReadDbContext _dbContext;

    public GetByIdDepartament(IReadDbContext dbContext) => _dbContext = dbContext;
}