using FileService.Core.abstractions;
using FileService.Infrastructure.Postgres.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine.EntityFrameworkCore;

namespace FileService.Infrastructure.Postgres.Repositori
{
    public sealed class OutboxServise : IOutboxServise
    {
        private readonly IDbContextOutbox<PostgresDbContext> _outbox;

        public OutboxServise(IDbContextOutbox<PostgresDbContext> outbox)
        {
            _outbox = outbox;
        }

        public Task FlushAsync() => _outbox.FlushOutgoingMessagesAsync();

        public async Task PublishAsync<T>(T message) where T : class => await _outbox.PublishAsync(message);
    }
}
