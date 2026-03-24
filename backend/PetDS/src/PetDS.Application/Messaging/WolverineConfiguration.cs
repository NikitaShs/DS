using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.Postgresql;
using static CSharpFunctionalExtensions.Result;

namespace PetDS.Application.Messaging
{
    public static class WolverineConfiguration
    {
        public static void ConfigAddWolverine(this IServiceCollection services, IConfiguration configuration)
        {

            string rebbitString = configuration.GetConnectionString("RabbitMQ");

            string postgreConnectionString = configuration.GetConnectionString("BDDS");


            services.AddWolverine(ExtensionDiscovery.ManualOnly, opts =>
            {
                opts.ApplicationAssembly = typeof(WolverineConfiguration).Assembly;

                opts.ConfigureDurableMessaging(postgreConnectionString);

                opts.AddRebbitMQ(rebbitString);

                opts.AutoBuildMessageStorageOnStartup = JasperFx.AutoCreate.CreateOrUpdate;

                opts.Policies.UseDurableOutboxOnAllSendingEndpoints();

            });


        }

        private static void ConfigureDurableMessaging(this WolverineOptions opts, string postgreConnectionString)
        {
            opts.PersistMessagesWithPostgresql(postgreConnectionString, "public");
            opts.UseEntityFrameworkCoreTransactions();
            opts.Policies.UseDurableOutboxOnAllSendingEndpoints();

            opts.Policies.UseDurableInboxOnAllListeners();
        }
    }
}
