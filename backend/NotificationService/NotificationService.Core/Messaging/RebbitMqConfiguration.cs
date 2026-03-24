using Core.MessagingComminication.MessagingDto.Departament;
using Core.MessagingComminication.MessagingDto.Directory;
using Core.MessagingComminication.MessagingDto.File;
using Core.MessagingComminication.MessegingConstans;
using Wolverine;
using Wolverine.RabbitMQ;

namespace PetDS.Application.Messaging
{
    public static class RebbitMqConfiguration
    {
        public const string QUEUE_NOTIFICATION_DIRECTORY_EVENT = "queue.notification.departament.event";

        public const string ROUTING = "#";

        public static void AddRebbitMQ(this WolverineOptions opts, string connectionString)
        {
            opts.UseRabbitMq(new Uri(connectionString))
                .AutoProvision()
                .EnableWolverineControlQueues().UseQuorumQueues().DeclareExchange(MessagingCommunicationConstans.EXTENTION_DEPARTAMENTS_EVENTS, exchange =>
                {
                    exchange.ExchangeType = ExchangeType.Topic;

                    exchange.IsDurable = true;
                });


            opts.SetingListener();
            opts.SetingPublic();
        }

        private static void SetingListener(this WolverineOptions opts)
        {
            opts.ListenToRabbitQueue(QUEUE_NOTIFICATION_DIRECTORY_EVENT, queue =>
            {
                queue.BindExchange(MessagingCommunicationConstans.EXTENTION_DEPARTAMENTS_EVENTS, ROUTING);
            }).UseDurableInbox();
        }

        private static void SetingPublic(this WolverineOptions opts)
        {

        }
    }
}