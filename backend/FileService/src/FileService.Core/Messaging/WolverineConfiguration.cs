using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine;
using Wolverine.EntityFrameworkCore;
using Wolverine.Postgresql;
using static CSharpFunctionalExtensions.Result;

namespace FileService.Core.Messaging
{
    public static class WolverineConfiguration
    {
        public static void AddWolverine(this WebApplicationBuilder builder)
        {

            string rebbitString = builder.Configuration.GetConnectionString("RabbitMQ");

            string postgreConnectionString = builder.Configuration.GetConnectionString("BDFS");

            builder.Host.UseWolverine(opts =>
            {
                opts.ApplicationAssembly = typeof(WolverineConfiguration).Assembly;

                opts.AddRebbitMQ(rebbitString);

                opts.AutoBuildMessageStorageOnStartup = JasperFx.AutoCreate.CreateOrUpdate;

                opts.ConfigureDurableMessaging(postgreConnectionString);
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
