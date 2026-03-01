using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine;
using static CSharpFunctionalExtensions.Result;

namespace FileService.Core.Messaging
{
    public static class WolverineConfiguration
    {
        public static void AddWolverine(this WebApplicationBuilder builder)
        {

            string rebbitString = builder.Configuration.GetConnectionString("RabbitMQ");

            builder.Host.UseWolverine(opts =>
            {
                opts.ApplicationAssembly = typeof(WolverineConfiguration).Assembly;

                opts.AddRebbitMQ(rebbitString);

                opts.AutoBuildMessageStorageOnStartup = JasperFx.AutoCreate.CreateOrUpdate;
            });
        }
    }
}
