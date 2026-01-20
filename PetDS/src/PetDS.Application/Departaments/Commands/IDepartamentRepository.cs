using CSharpFunctionalExtensions;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location.VO;
using PetDS.Domain.Shered;
using SharedKernel.Exseption;

namespace PetDS.Application.Departaments;

public interface IDepartamentRepository
{
    Task<Result<Guid, Errors>> AddDepartament(Departament departament, CancellationToken cancellationToken);

    Task<Result<Departament, Errors>> GetDepartamentById(DepartamentId id, CancellationToken cancellationToken);

    Task<Result<List<Departament>, Errors>> GetDepartamentsById(List<DepartamentId> ids,
        CancellationToken cancellationToken);

    Task<Result<Guid, Errors>> UpdateLocations(List<LocationId> locationIds, DepartamentId departamentId,
        CancellationToken cancellationToken);

    Task<Result<int, Errors>> UpdateDepartamentFullHierahiById(Guid departamentId, Guid? parent_id,
        CancellationToken cancellationToken);

    Task<Result<bool, Errors>> CheckingDepartamentExistence(DepartamentId departamentId,
        CancellationToken cancellationToken);

    Task<Result<bool, Errors>> SoftDeleteDept(Guid departamentId, CancellationToken cancellationToken);
}