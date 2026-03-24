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
        public const string QUEUE_DEPARTAMENTS_FILE_EVENT = "queue.departaments.file.event";

        public const string ROUTING_DEPARTAMENT_ALL_EVENT = "*.*.departament";

        public static void AddRebbitMQ(this WolverineOptions opts, string connectionString)
        {
            opts.UseRabbitMq(new Uri(connectionString))
                .AutoProvision()
                .EnableWolverineControlQueues()
                .UseQuorumQueues().DeclareExchange(MessagingCommunicationConstans.EXTENTION_FILE_EVENTS, exchange =>
                {
                    exchange.ExchangeType = ExchangeType.Topic;

                    exchange.IsDurable = true;
                }).UseQuorumQueues().DeclareExchange(MessagingCommunicationConstans.EXTENTION_DEPARTAMENTS_EVENTS, exchange =>
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
            }).UseDurableInbox();
        }

        private static void SetingPublic(this WolverineOptions opts)
        {
            opts.PublishMessagesToRabbitMqExchange<DepartamentDelete>(MessagingCommunicationConstans.EXTENTION_DEPARTAMENTS_EVENTS,
                m => string.Empty).UseDurableOutbox();

            opts.PublishMessagesToRabbitMqExchange<DepartamentCreate>(MessagingCommunicationConstans.EXTENTION_DEPARTAMENTS_EVENTS,
                m => MessagingCommunicationConstans.ROUTING_CREATE_DEPARTAMENT).UseDurableOutbox();

            opts.PublishMessagesToRabbitMqExchange<DepartmentDeactivated>(MessagingCommunicationConstans.EXTENTION_DEPARTAMENTS_EVENTS,
                m => MessagingCommunicationConstans.ROUTING_DEACTIVATED_DEPARTAMENT).UseDurableOutbox();

            opts.PublishMessagesToRabbitMqExchange<DepartmentLocationsUpdated>(MessagingCommunicationConstans.EXTENTION_DEPARTAMENTS_EVENTS,
                m => MessagingCommunicationConstans.ROUTING_LOCATION_UPDATE_DEPARTAMENT).UseDurableOutbox();

            opts.PublishMessagesToRabbitMqExchange<PositionCreated>(MessagingCommunicationConstans.EXTENTION_DEPARTAMENTS_EVENTS,
                m => MessagingCommunicationConstans.ROUTING_CREATE_POSITION).UseDurableOutbox();

            opts.PublishMessagesToRabbitMqExchange<PositionDeleted>(MessagingCommunicationConstans.EXTENTION_DEPARTAMENTS_EVENTS,
                m => MessagingCommunicationConstans.ROUTING_DELETE_POSITION).UseDurableOutbox();
        }
    }
}