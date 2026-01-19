using CSharpFunctionalExtensions;
using SharedKernel.Exseption;

namespace Core.adstract;

public interface IConnectionManeger
{
    Task<Result<ITransactionScopes, Error>> CreateTranzit(CancellationToken cancellationToken);

    Task<UnitResult<Error>> SaveChangeAsync();
}