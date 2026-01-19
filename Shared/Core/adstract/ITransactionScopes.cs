using CSharpFunctionalExtensions;
using SharedKernel.Exseption;

namespace Core.adstract;

public interface ITransactionScopes : IDisposable
{
    UnitResult<Error> Commit();

    void Rollback();
}
