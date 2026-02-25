using FileService.Domain.Entites;
using ImTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wolverine;
using Wolverine.RabbitMQ;

namespace FileService.Core.Messaging
{
    public static class RebbitMqConfiguration
    {
        public static void AddRebbitMQ(this WolverineOptions opts, string connectionString)
        {
            opts.UseRabbitMq(new Uri(connectionString))
                .AutoProvision()
                .EnableWolverineControlQueues()
                .UseQuorumQueues()
                .DeclareExchange("exchange", exchange =>
                {
                    exchange.ExchangeType = ExchangeType.Topic;

                    exchange.IsDurable = true;
                });


            opts.НастройкаПубликацииРоутинг();

            opts.PublishMessagesToRabbitMqExchange<VideoAsset>(
                "exchange",
                m => $"{VideoAsset.BUCKET}").UseDurableOutbox();
        }

        private static void НастройкаПубликацииРоутинг(this WolverineOptions opts)
        {
            opts.PublishMessagesToRabbitMqExchange<VideoAsset>(
                "exchange",
                m => $"{VideoAsset.BUCKET}").UseDurableOutbox();
        }

    }
}
