using CSharpFunctionalExtensions;
using PetDS.Domain.Shered;

namespace PetDS.Application.abcstractions;

public interface IConnectionManeger
{
    Task<Result<ITransactionScopes, Error>> CreateTranzit(CancellationToken cancellationToken);

    Task<UnitResult<Error>> SaveChangeAsync();
}