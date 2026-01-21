using CSharpFunctionalExtensions;
using SharedKernel.Exseption;

namespace Core.Adstract;

public interface ITransactionScopes : IDisposable
{
    UnitResult<Error> Commit();

    void Rollback();
}
