using Core.MessagingComminication.MessagingDto.Departament;
using Core.MessagingComminication.MessagingDto.Directory;
using Microsoft.Extensions.Logging;
using NotificationService.Core.Abstraction;
using NotificationService.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NotificationService.Core.IntegrationEvents
{
    public class PositionCreatedHandler
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<PositionCreatedHandler> _logger;

        public PositionCreatedHandler(INotificationRepository notificationRepository, ILogger<PositionCreatedHandler> logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        public async Task Handle(PositionCreated positionCreated, CancellationToken cancellationToken)
        {
            var resCheck = await _notificationRepository.CheckDuplicatNotification("department.created", positionCreated.posId, cancellationToken);
            if (resCheck.IsFailure)
            {
                _logger.LogError("да такое смс уже было");
                return;
            }

            var resInApp = Notification.CreateInApp(
                null,
                "positionCreated",
                $"Создана новая позиция: {positionCreated.name}",
                "position.created",
                positionCreated.posId,
                null,
                DateTime.UtcNow
                );

            if (resInApp.IsFailure)
            {
                _logger.LogError("неудалось создать In-App сообщение с созданием позиции в id {id}", positionCreated.posId);
                return;
            }

            var res = await _notificationRepository.AddNotification(resInApp.Value, cancellationToken);

            if (res.IsFailure)
            {
                _logger.LogError("неудалось схранить In-App сообщение с созданием позиции в id {id}", positionCreated.posId);
                return;
            }
        }
    }
}
