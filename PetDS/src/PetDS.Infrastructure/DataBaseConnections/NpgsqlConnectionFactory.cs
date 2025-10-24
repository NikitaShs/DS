using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Infrastructure.DataBaseConnections
{
    public class NpgsqlConnectionFactory : IConnectionFactory, IDisposable, IAsyncDisposable
    {
        private readonly NpgsqlDataSource _dataSource;

        public NpgsqlConnectionFactory(IConfiguration configuration)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("BDDS"));
            dataSourceBuilder.UseLoggerFactory(CreateLoggerFactory());
            _dataSource = dataSourceBuilder.Build();
        }

        public async Task<DbConnection> CreateConnectionAsync(CancellationToken cancellationToken)
        {
            return await _dataSource.OpenConnectionAsync(cancellationToken);
        }

        public ILoggerFactory CreateLoggerFactory() => LoggerFactory.Create(q => q.AddConsole());

        public void Dispose() => ((IDisposable)_dataSource).Dispose();

        public ValueTask DisposeAsync() => ((IAsyncDisposable)_dataSource).DisposeAsync();
    }
}
