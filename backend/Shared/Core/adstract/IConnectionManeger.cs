using CSharpFunctionalExtensions;
using SharedKernel.Exseption;

namespace Core.Adstract;

public interface IConnectionManeger : IAsyncDisposable
{
    Task<Result<IConnectionManeger, Errors>> CreateTranzit(CancellationToken cancellationToken);

    Task<UnitResult<Error>> SaveChangeAsync(CancellationToken cancellationToken);

    Task<UnitResult<Error>> Commit(CancellationToken cancellationToken);

    Task Rollback(CancellationToken cancellationToken);

    Task<UnitResult<Error>> PublishToOutboxAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;

}