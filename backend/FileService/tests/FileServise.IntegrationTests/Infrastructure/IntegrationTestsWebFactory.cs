using Amazon.S3;
using DotNet.Testcontainers.Builders;
using FileService.Core.abstractions;
using FileService.Infrastructure.Postgres.DataBase;
using FileService.Infrastructure.S3;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Npgsql;
using Respawn;
using System.Data.Common;
using Testcontainers.Minio;
using Testcontainers.PostgreSql;

namespace FileServise.IntegrationTests.Infrastructure;

public class IntegrationTestsWebFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresDockerConteiner = new PostgreSqlBuilder()
        .WithImage("postgres")
        .WithDatabase("file_servise_tests")
        .WithUsername("posgre")
        .WithPassword("posgre")
        .Build();

    private readonly MinioContainer _minioDockerContainer = new MinioBuilder()
        .WithImage("minio/minio")
        .WithUsername("user")
        .WithPassword("password")
        .Build();

    private DbConnection _dbConnection = null!;
    private Respawner _respawner = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.Tests.json"), optional: true);
        });

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<PostgresDbContext>();

            services.AddScoped<PostgresDbContext>(q =>
                new PostgresDbContext(_postgresDockerConteiner.GetConnectionString())
            );

            services.RemoveAll<IAmazonS3>();

            services.AddSingleton<IAmazonS3>(sp =>
            {
                S3Options s3Options = sp.GetRequiredService<IOptions<S3Options>>().Value;

                ushort minioPort = _minioDockerContainer.GetMappedPublicPort(9000);

                var configure = new AmazonS3Config
                {
                    ServiceURL = $"http://{_minioDockerContainer.Hostname}:{minioPort}",
                    UseHttp = true,
                    ForcePathStyle = true
                };

                return new AmazonS3Client(s3Options.AccessKey, s3Options.SecretKey, configure);
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _postgresDockerConteiner.StartAsync();
        await _minioDockerContainer.StartAsync();

        await using AsyncServiceScope scope = Services.CreateAsyncScope();

        PostgresDbContext dbContext = scope.ServiceProvider.GetRequiredService<PostgresDbContext>();

        await dbContext.Database.EnsureDeletedAsync();

        await dbContext.Database.EnsureCreatedAsync();

        _dbConnection = new NpgsqlConnection(_postgresDockerConteiner.GetConnectionString());
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(
            _dbConnection,
            new RespawnerOptions { DbAdapter = DbAdapter.Postgres,SchemasToInclude = ["public"] });

    }

    public async Task DisposeAsync()
    {
        await _postgresDockerConteiner.StopAsync();
        await _postgresDockerConteiner.DisposeAsync();

        await _minioDockerContainer.StopAsync();
        await _minioDockerContainer.DisposeAsync();

        await _dbConnection.CloseAsync();
        await _dbConnection.DisposeAsync();
    }

    public async Task ResetDataBase() => await _respawner.ResetAsync(_dbConnection);

}