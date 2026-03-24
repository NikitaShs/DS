using Core.Adstract;
using Core.MessagingComminication.MessagingDto;
using Core.MessagingComminication.MessagingDto.Departament;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Shered;
using SharedKernel.Exseption;
using Wolverine;

namespace PetDS.Application.Departaments.Commands.DeleteDepartament;

public class DeleteDepartamentServise : IHandler<Guid, DeleteDepartamentCommand>
{
    private readonly IConnectionManeger _connectionManeger;
    private readonly IDepartamentRepository _departamentRepository;
    private readonly HybridCache _hybridCache;
    private readonly ILogger<DeleteDepartamentServise> _logger;


    public DeleteDepartamentServise(
        ILogger<DeleteDepartamentServise> logger,
        IDepartamentRepository departamentRepository,
        IConnectionManeger connectionManeger,
        HybridCache hybridCache)
    {
        _logger = logger;
        _departamentRepository = departamentRepository;
        _connectionManeger = connectionManeger;
        _hybridCache = hybridCache;
    }

    public async Task<Result<Guid, Errors>> Handler(DeleteDepartamentCommand command,
        CancellationToken cancellationToken)
    {

        var resTranz = await _connectionManeger.CreateTranzit(cancellationToken);

        if (resTranz.IsFailure)
        {
            return resTranz.Error;
        }

        await using var tranzit = resTranz.Value;

        if (!_departamentRepository
                .CheckingDepartamentExistence(DepartamentId.Create(command.departamenId), cancellationToken).Result
                .Value)
        {
            _logger.LogInformation("департамент не существует");
            return GeneralErrors.ValueNotValid("departamenId").ToErrors();
        }

        Result<bool, Errors> res = await _departamentRepository.SoftDeleteDept(command.departamenId, cancellationToken);
        if (res.IsFailure)
        {
            return GeneralErrors.Update("SoftDeleteDept").ToErrors();
        }

        var dept = await _departamentRepository.GetDepartamentById(DepartamentId.Create(command.departamenId), cancellationToken);
        if (dept.IsFailure)
        {
            _logger.LogInformation("не найден такой департмаент");
            return GeneralErrors.Update("SoftDeleteDept").ToErrors();
        }


        await tranzit.PublishToOutboxAsync(new DepartmentDeactivated(dept.Value.Id.ValueId, dept.Value.Name.ValueName));

        await tranzit.Commit(cancellationToken);

        _hybridCache.RemoveByTagAsync(CacheTags.Departament);

        return command.departamenId;
    }
}