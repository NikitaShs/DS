using FileService.Infrastructure.Postgres.DataBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileServise.IntegrationTests.Infrastructure
{
    public class FileServiceTestsBase : IAsyncLifetime, IClassFixture<IntegrationTestsWebFactory>
    {

        protected IServiceProvider Services { get; init; }

        protected HttpClient HttpClient { get; init; }

        protected HttpClient AppHttpClient { get; init; }

        protected Func<Task> ResetBd { get; init; }

        public FileServiceTestsBase(IntegrationTestsWebFactory factory)
        {
            Services = factory.Services;
            AppHttpClient = factory.CreateClient();
            HttpClient = new HttpClient();
            ResetBd = factory.ResetDataBase;
        }

        protected async Task ExecuteInDb(Func<PostgresDbContext, Task> action)
        {
            await using AsyncServiceScope scope = Services.CreateAsyncScope();

            PostgresDbContext dbContext = scope.ServiceProvider.GetService<PostgresDbContext>();

            await action(dbContext);
        }

        public Task InitializeAsync() => Task.CompletedTask;

        public async Task DisposeAsync() => await ResetBd();
    }
}
