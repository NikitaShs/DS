using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using PetDS.Infrastructure.DataBaseConnections;
using PetDS.Web;
using Respawn;
using Testcontainers.PostgreSql;

namespace PetDSTests;

// IAsyncLifetime для асинхронного использования в онологии конструктора
public class FactoryTest : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresDockerConteiner = new PostgreSqlBuilder()
        .WithImage("postgres")
        .WithDatabase("nu")
        .WithUsername("now")
        .WithPassword("grad")
        .Build();

    private DbConnection _dbConnection = null!;
    private Respawner _respawner = null!;


    public async Task InitializeAsync()
    {
        await _postgresDockerConteiner.StartAsync();

        await using AsyncServiceScope scope = Services.CreateAsyncScope();

        ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.EnsureDeletedAsync();

        await dbContext.Database.EnsureCreatedAsync();

        _dbConnection = new NpgsqlConnection(_postgresDockerConteiner.GetConnectionString());
        await _dbConnection.OpenAsync();
        await InitializeRespawner();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _postgresDockerConteiner.StopAsync();
        await _postgresDockerConteiner.DisposeAsync();
        await _dbConnection.CloseAsync();
        await _dbConnection.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<ApplicationDbContext>();

            services.AddScoped<ApplicationDbContext>(q =>
                new ApplicationDbContext(_postgresDockerConteiner.GetConnectionString())
            );
        });

    public async Task ResetDataBase() => await _respawner.ResetAsync(_dbConnection);

    private async Task InitializeRespawner() =>
        _respawner = await Respawner.CreateAsync(
            _dbConnection,
            new RespawnerOptions { DbAdapter = DbAdapter.Postgres, SchemasToInclude = ["public"] });
}