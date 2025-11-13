using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Shered;

namespace PetDS.Application.Departaments.UpdateDepartament.UpdateDepartamentDepartamentHierarchy;

public class UpdateDepartamentHierarchyServise : IHandler<Guid, UpdateDepartamentHierarchyCommand>
{
    private readonly IConnectionManeger _connectionManeger;

    private readonly IDepartamentRepository _departamentRepository;
    private readonly ILogger<UpdateDepartamentHierarchyServise> _logger;

    public UpdateDepartamentHierarchyServise(
        ILogger<UpdateDepartamentHierarchyServise> logger,
        IDepartamentRepository departamentRepository,
        IConnectionManeger connectionManeger)
    {
        _logger = logger;
        _departamentRepository = departamentRepository;
        _connectionManeger = connectionManeger;
    }

    public async Task<Result<Guid, Errors>> Handler(UpdateDepartamentHierarchyCommand command,
        CancellationToken cancellationToken = default)
    {
        DepartamentId w = DepartamentId.Create(command.departanetId);
        Result<Departament, Errors> ret = _departamentRepository.GetDepartamentFullHierahiById(w, cancellationToken)
            .Result;
        return DepartamentId.CreateNewGuid().ValueId;
    }
}