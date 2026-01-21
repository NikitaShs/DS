using System.Data;
using Core.Adstract;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using SharedKernel.Exseption;

namespace PetDS.Infrastructure.DataBaseConnections;

public class TransactionScopes : ITransactionScopes, IDisposable
{
    private readonly IDbTransaction _dbtransation;
    private readonly ILogger<TransactionScopes> _logger;

    public TransactionScopes(IDbTransaction dbtransation, ILogger<TransactionScopes> logger)
    {
        _dbtransation = dbtransation;
        _logger = logger;
    }

    public UnitResult<Error> Commit()
    {
        try
        {
            _dbtransation.Commit();
            _logger.LogInformation("успешный комит");
            return UnitResult.Success<Error>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "успешный комит");
            return Error.Failure("fail.commit", "fail is commit");
        }
    }

    public void Dispose() => _dbtransation?.Dispose();

    public void Rollback() => _dbtransation.Rollback();
}