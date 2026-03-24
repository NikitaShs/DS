using Core.Adstract;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Serilog;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Error = SharedKernel.Exseption.Error;

namespace NotificationService.Infrastructure.DataBaseConnection
{
    public class ConnectionManeger : IAsyncDisposable, IConnectionManeger
    {
        private readonly IDbContextOutbox<NotificationDbContext> _outbox;
        private readonly ILogger<ConnectionManeger> _logger;
        private IDbContextTransaction _dbtransation;

        public ConnectionManeger(IDbContextOutbox<NotificationDbContext> outbox, ILogger<ConnectionManeger> logger)
        {
            _logger = logger;
            _outbox = outbox;
        }

        public async Task<Result<IConnectionManeger, Errors>> CreateTranzit(CancellationToken cancellationToken)
        {
            try
            {
                _dbtransation = await _outbox.DbContext.Database.BeginTransactionAsync(cancellationToken);
                return this;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "транзакция не создана ");
                return Error.Failure("transaction.failed", ex.Message).ToErrors();
            }
        }

        public async Task<UnitResult<Error>> Commit(CancellationToken cancellationToken)
        {
            if (_dbtransation is null)
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

        public Task Rollback(CancellationToken cancellationToken) => _dbtransation.RollbackAsync(cancellationToken);

        public ValueTask DisposeAsync() => _dbtransation.DisposeAsync();

        public async Task<UnitResult<Error>> SaveChangeAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _outbox.DbContext.SaveChangesAsync(cancellationToken);
                return UnitResult.Success<Error>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ошибка в созранением");
                return Error.Failure("fail.is.save", "fail save");
            }
        }

        public async Task<UnitResult<Error>> PublishToOutboxAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
        {
            if (_dbtransation == null)
                return Error.Failure("transaction.not.started", "Транзакция не начата");

            try
            {
                await _outbox.PublishAsync(message);

                _logger.LogDebug("Сообщение сохранено в Outbox");
                return UnitResult.Success<Error>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при сохранении сообщения в Outbox");
                return Error.Failure("fail.is.save.Outbox", "fail save Outbox");
            }
        }

    }
}

