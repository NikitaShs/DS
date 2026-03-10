using Core.Adstract;
using CSharpFunctionalExtensions;
using FileService.Infrastructure.Postgres.DataBase;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using SharedKernel.Exseption;
using System.Data;
using System.Data.Common;
using Wolverine.EntityFrameworkCore;

namespace PetDS.Infrastructure.DataBaseConnections;

public class ConnectionManeger : IDisposable
{
    private readonly IDbContextOutbox<PostgresDbContext> _outbox;
    private readonly ILogger<ConnectionManeger> _logger;
    private IDbContextTransaction _dbtransation;

    public ConnectionManeger(IDbContextOutbox<PostgresDbContext> outbox, ILogger<ConnectionManeger> logger)
    {
        _logger = logger;
        _outbox = outbox;
    }

    public async Task<UnitResult<Error>> CreateTranzit(CancellationToken cancellationToken)
    {
        try
        {
            _dbtransation = await _outbox.DbContext.Database.BeginTransactionAsync(cancellationToken);
            return UnitResult.Success<Error>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "транзакция не создана ");
            return GeneralErrors.Unknown();
        }
    }

    public async Task<UnitResult<Error>> Commit(CancellationToken cancellationToken)
    {
        if(_dbtransation is null)
            return Error.Failure("fail.commit", "fail is commit");
        try
        {
            await _dbtransation.CommitAsync(cancellationToken);
            await _outbox.FlushOutgoingMessagesAsync();
            _logger.LogInformation("успешный комит");
            return UnitResult.Success<Error>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "не успешный комит");
            return Error.Failure("fail.commit", "fail is commit");
        }
    }

    public void Dispose() => _dbtransation?.Dispose();

    public void Rollback() => _dbtransation.Rollback();


    public async Task<UnitResult<Error>> SaveChangeAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _outbox.DbContext.SaveChangesAsync(cancellationToken);
            await _outbox.FlushOutgoingMessagesAsync();
            return UnitResult.Success<Error>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ошибка в созранением");
            return Error.Failure("fail.is.save", "fail save");
        }
    }
}
