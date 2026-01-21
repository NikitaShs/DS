using Core.Adstract;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Shered;
using SharedKernel.Exseption;

namespace PetDS.Application.Departaments.Commands.DeleteDepartament;

public class DeleteDepartamentServise : IHandler<Guid, DeleteDepartamentCommand>
{
    private readonly IConnectionManeger _connectionManeger;
    private readonly IDepartamentRepository _departamentRepository;
    private readonly HybridCache _hybridCache;
    private readonly ILogger<DeleteDepartamentServise> _logger;


    public DeleteDepartamentServise(ILogger<DeleteDepartamentServise> logger,
        IDepartamentRepository departamentRepository,
        IConnectionManeger connectionManeger, HybridCache hybridCache)
    {
        _logger = logger;
        _departamentRepository = departamentRepository;
        _connectionManeger = connectionManeger;
        _hybridCache = hybridCache;
    }

    public async Task<Result<Guid, Errors>> Handler(DeleteDepartamentCommand command,
        CancellationToken cancellationToken)
    {
        if (!_departamentRepository
                .CheckingDepartamentExistence(DepartamentId.Create(command.departamenId), cancellationToken).Result
                .Value)
        {
            _logger.LogInformation("департамент не существует");
            return GeneralErrors.ValueNotValid("departamenId").ToErrors();
        }

        using ITransactionScopes? tran = _connectionManeger.CreateTranzit(cancellationToken).Result.Value;


        Result<bool, Errors> res = await _departamentRepository.SoftDeleteDept(command.departamenId, cancellationToken);

        if (res.IsFailure)
        {
            return GeneralErrors.Update("SoftDeleteDept").ToErrors();
        }

        tran.Commit();
        _hybridCache.RemoveByTagAsync(CacheTags.Departament);
        return command.departamenId;
    }
}