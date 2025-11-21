using CSharpFunctionalExtensions;
using PetDS.Domain.Shered;

namespace PetDS.Application.abcstractions;

public interface ITransactionScopes : IDisposable
{
    UnitResult<Error> Commit();

    void Rollback();
}