using Core.MessagingComminication.MessagingDto;
using Core.MessagingComminication.MessegingConstans;
using Wolverine;
using Wolverine.RabbitMQ;

namespace PetDS.Application.Messaging
{
    public static class RebbitMqConfiguration
    {
        public const string QUEUE_DEPARTAMENTS_FILE_EVENT = "queue.departaments.file.event";

        public const string ROUTING_DEPARTAMENT_ALL_EVENT = "*.*.departament";

        public static void AddRebbitMQ(this WolverineOptions opts, string connectionString)
        {
            opts.UseRabbitMq(new Uri(connectionString))
                //.AutoProvision()
                .EnableWolverineControlQueues()
                .UseQuorumQueues().DeclareExchange(MessagingCommunicationConstans.EXTENTION_FILE_EVENTS, exchange =>
                {
                    exchange.ExchangeType = ExchangeType.Topic;

                    exchange.IsDurable = true;
                });

            opts.SetingListener();
            opts.SetingPublic();
        }

        private static void SetingListener(this WolverineOptions opts)
        {
            opts.ListenToRabbitQueue(QUEUE_DEPARTAMENTS_FILE_EVENT, queue =>
            {
                queue.BindExchange(MessagingCommunicationConstans.EXTENTION_FILE_EVENTS, ROUTING_DEPARTAMENT_ALL_EVENT);
            });
        }

        private static void SetingPublic(this WolverineOptions opts)
        {
            opts.PublishMessagesToRabbitMqExchange<DepartamentDelete>(MessagingCommunicationConstans.EXTENTION_DEPARTAMENTS_EVENTS,
                m => string.Empty);
        }
    }
}