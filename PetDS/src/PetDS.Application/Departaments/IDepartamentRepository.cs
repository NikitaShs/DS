using CSharpFunctionalExtensions;
using PetDS.Domain.Shered;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location.VO;

namespace PetDS.Application.Departaments
{
    public interface IDepartamentRepository
    {
        Task<Result<Guid, Errors>> AddDepartament(Departament departament, CancellationToken cancellationToken);

        Task<Result<Departament, Errors>> GetDepartamentById(DepartamentId id, CancellationToken cancellationToken);

        Task<Result<List<Departament>, Errors>> GetDepartamentsById(List<DepartamentId> ids, CancellationToken cancellationToken);

    }
}
