using CSharpFunctionalExtensions;
using SharedKernel.Exseption;

namespace Core.Adstract;

public interface IConnectionManeger
{
    Task<Result<ITransactionScopes, Error>> CreateTranzit(CancellationToken cancellationToken);

    Task<UnitResult<Error>> SaveChangeAsync();
}