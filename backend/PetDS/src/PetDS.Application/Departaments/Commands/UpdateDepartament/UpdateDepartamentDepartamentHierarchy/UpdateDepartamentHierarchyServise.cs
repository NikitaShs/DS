using Core.Adstract;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Shered;
using SharedKernel.Exseption;

namespace PetDS.Application.Departaments.UpdateDepartament.UpdateDepartamentDepartamentHierarchy;

public class UpdateDepartamentHierarchyServise : IHandler<Guid, UpdateDepartamentHierarchyCommand>
{
    private readonly IConnectionManeger _connectionManeger;

    private readonly IDepartamentRepository _departamentRepository;
    private readonly HybridCache _hybridCache;
    private readonly ILogger<UpdateDepartamentHierarchyServise> _logger;


    public UpdateDepartamentHierarchyServise(
        ILogger<UpdateDepartamentHierarchyServise> logger,
        IDepartamentRepository departamentRepository,
        IConnectionManeger connectionManeger,
        HybridCache hybridCache)
    {
        _logger = logger;
        _departamentRepository = departamentRepository;
        _connectionManeger = connectionManeger;
        _hybridCache = hybridCache;
    }

    public async Task<Result<Guid, Errors>> Handler(
        UpdateDepartamentHierarchyCommand command,
        CancellationToken cancellationToken = default)
    {
        Result<ITransactionScopes, Error> resultCommand = await _connectionManeger.CreateTranzit(cancellationToken);
        if (resultCommand.IsFailure)
        {
            return GeneralErrors.Unknown().ToErrors();
        }

        using ITransactionScopes? tranziction = resultCommand.Value;

        if (command.departanetId == command.dto.parantId)
        {
            return GeneralErrors.Unknown().ToErrors();
        }

        if (command.dto.parantId != null)
        {
            Result<bool, Errors> resParent =
                await _departamentRepository.CheckingDepartamentExistence(
                    DepartamentId.Create(command.dto.parantId.Value), cancellationToken);
            if (resParent.IsFailure || !resParent.Value)
            {
                _logger.LogInformation("parantId нету");
                return GeneralErrors.Unknown().ToErrors();
            }
        }

        Result<bool, Errors> res =
            await _departamentRepository.CheckingDepartamentExistence(DepartamentId.Create(command.departanetId),
                cancellationToken);
        if (res.IsFailure || !res.Value)
        {
            _logger.LogInformation("departanetId нету");
            return GeneralErrors.Unknown().ToErrors();
        }

        Result<int, Errors> result =
            await _departamentRepository.UpdateDepartamentFullHierahiById(command.departanetId, command.dto.parantId,
                cancellationToken);
        if (result.Value <= 0)
        {
            return GeneralErrors.Update("Hierahi").ToErrors();
        }

        tranziction.Commit();
        _hybridCache.RemoveByTagAsync(CacheTags.Departament);
        return command.departanetId;
    }
}