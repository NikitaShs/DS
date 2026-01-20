using System.Data.Common;
using Core.adstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using PetDS.Application.abcstractions;

namespace PetDS.Infrastructure.DataBaseConnections;

public class NpgsqlConnectionFactory : IConnectionFactory, IDisposable, IAsyncDisposable
{
    private readonly NpgsqlDataSource _dataSource;

    public NpgsqlConnectionFactory(IConfiguration configuration)
    {
        NpgsqlDataSourceBuilder dataSourceBuilder = new(configuration.GetConnectionString("BDDS"));
        dataSourceBuilder.UseLoggerFactory(CreateLoggerFactory());
        _dataSource = dataSourceBuilder.Build();
    }

    public ValueTask DisposeAsync() => ((IAsyncDisposable)_dataSource).DisposeAsync();

    public async Task<DbConnection> CreateConnectionAsync(CancellationToken cancellationToken) =>
        await _dataSource.OpenConnectionAsync(cancellationToken);

    public void Dispose() => ((IDisposable)_dataSource).Dispose();

    public ILoggerFactory CreateLoggerFactory() => LoggerFactory.Create(q => q.AddConsole());
}