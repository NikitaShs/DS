using Core.MessagingComminication.MessagingDto;
using Core.MessagingComminication.MessegingConstans;
using Wolverine;
using Wolverine.RabbitMQ;

namespace FileService.Core.Messaging
{
    public static class RebbitMqConfiguration
    {
        public static void AddRebbitMQ(this WolverineOptions opts, string connectionString)
        {
            opts.UseRabbitMq(new Uri(connectionString))
                .EnableWolverineControlQueues()
                .UseQuorumQueues()
                .DeclareExchange(MessagingCommunicationConstans.EXTENTION_FILE_EVENTS, exchange =>
                {
                    exchange.ExchangeType = ExchangeType.Topic;

                    exchange.IsDurable = true;
                });


            opts.SetingPublic();

        }

        private static void SetingPublic(this WolverineOptions opts)
        {
            opts.PublishMessagesToRabbitMqExchange<VideoCreate>(
                MessagingCommunicationConstans.EXTENTION_FILE_EVENTS,
                m => $"{MessagingCommunicationConstans.ROUTING_CREATE_VIDEO}.{m.EntiteType}").UseDurableOutbox();

            opts.PublishMessagesToRabbitMqExchange<VideoDelete>(
                MessagingCommunicationConstans.EXTENTION_FILE_EVENTS,
                m => $"{MessagingCommunicationConstans.ROUTING_DELETE_VIDEO}.{m.EntiteType}").UseDurableOutbox();

            opts.PublishMessagesToRabbitMqExchange<ImageCreate>(
                MessagingCommunicationConstans.EXTENTION_FILE_EVENTS,
                m => $"{MessagingCommunicationConstans.ROUTING_CREATE_IMAGE}.{m.EntiteType}").UseDurableOutbox();

            opts.PublishMessagesToRabbitMqExchange<ImageDelete>(
                MessagingCommunicationConstans.EXTENTION_FILE_EVENTS,
                m => $"{MessagingCommunicationConstans.ROUTING_DELETE_IMAGE}.{m.EntiteType}").UseDurableOutbox();

            opts.PublishMessagesToRabbitMqExchange<PreviewCreate>(
                MessagingCommunicationConstans.EXTENTION_FILE_EVENTS,
                m => $"{MessagingCommunicationConstans.ROUTING_CREATE_PREVIEW}.{m.EntiteType}").UseDurableOutbox();

            opts.PublishMessagesToRabbitMqExchange<PreviewDelete>(
                MessagingCommunicationConstans.EXTENTION_FILE_EVENTS,
                m => $"{MessagingCommunicationConstans.ROUTING_DELETE_PREVIEW}.{m.EntiteType}").UseDurableOutbox();

            opts.PublishMessagesToRabbitMqExchange<AvatarCreate>(
                MessagingCommunicationConstans.EXTENTION_FILE_EVENTS,
                m => $"{MessagingCommunicationConstans.ROUTING_CREATE_AVATAR}.{m.EntiteType}").UseDurableOutbox();

            opts.PublishMessagesToRabbitMqExchange<AvatarDelete>(
                MessagingCommunicationConstans.EXTENTION_FILE_EVENTS,
                m => $"{MessagingCommunicationConstans.ROUTING_DELETE_AVATAR}.{m.EntiteType}").UseDurableOutbox();
        }

    }
}
