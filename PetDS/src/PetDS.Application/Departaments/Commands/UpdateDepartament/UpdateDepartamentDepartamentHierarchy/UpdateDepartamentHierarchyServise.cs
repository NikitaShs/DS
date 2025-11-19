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

    public async Task<Result<Guid, Errors>> Handler(
        UpdateDepartamentHierarchyCommand command,
        CancellationToken cancellationToken = default)
    {
        var resultCommand = await _connectionManeger.CreateTranzit(cancellationToken);
        if (resultCommand.IsFailure)
        {
            return GeneralErrors.Unknown().ToErrors();
        }

        using var tranziction = resultCommand.Value;

        if (command.departanetId == command.dto.parantId)
        {
            return GeneralErrors.Unknown().ToErrors();
        }

        if (command.dto.parantId != null)
        {
            var resParent = await _departamentRepository.CheckingDepartamentExistence(DepartamentId.Create((Guid)command.dto.parantId), cancellationToken);
            if (resParent.IsFailure || resParent.Value == false)
            {
                _logger.LogInformation("parantId нету");
                return GeneralErrors.Unknown().ToErrors();
            }
        }

        var res = await _departamentRepository.CheckingDepartamentExistence(DepartamentId.Create(command.departanetId), cancellationToken);
        if (res.IsFailure || res.Value == false)
        {
            _logger.LogInformation("departanetId нету");
            return GeneralErrors.Unknown().ToErrors();
        }

        var result = await _departamentRepository.UpdateDepartamentFullHierahiById(command.departanetId, command.dto.parantId, cancellationToken);

        tranziction.Commit();
        return result;
    }
}