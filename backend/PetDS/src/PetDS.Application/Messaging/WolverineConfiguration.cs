using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine;
using static CSharpFunctionalExtensions.Result;

namespace PetDS.Application.Messaging
{
    public static class WolverineConfiguration
    {
        public static void ConfigAddWolverine(this IServiceCollection services, IConfiguration configuration)
        {

            string rebbitString = configuration.GetConnectionString("RabbitMQ");

            services.AddWolverine(ExtensionDiscovery.ManualOnly, opts =>
            {
                opts.ApplicationAssembly = typeof(WolverineConfiguration).Assembly;

                opts.AddRebbitMQ(rebbitString);

                opts.AutoBuildMessageStorageOnStartup = JasperFx.AutoCreate.CreateOrUpdate;

                opts.Policies.UseDurableOutboxOnAllSendingEndpoints();

            });
        }
    }
}
