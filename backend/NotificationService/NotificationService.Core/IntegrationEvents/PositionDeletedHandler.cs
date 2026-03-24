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
    public sealed class PositionDeletedHandler
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<PositionDeletedHandler> _logger;

        public PositionDeletedHandler(INotificationRepository notificationRepository, ILogger<PositionDeletedHandler> logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        public async Task Handle(PositionDeleted positionDeleted, CancellationToken cancellationToken)
        {
            var resCheck = await _notificationRepository.CheckDuplicatNotification("department.created", positionDeleted.posId, cancellationToken);
            if (resCheck.IsFailure)
            {
                _logger.LogError("да такое смс уже было");
                return;
            }

            var resInApp = Notification.CreateInApp(
                null,
                "positionDeleted",
                $"Позиция {positionDeleted.name} была удалена",
                "department.delete",
                positionDeleted.posId,
                null,
                DateTime.UtcNow
                );

            if (resInApp.IsFailure)
            {
                _logger.LogError("неудалось создать In-App сообщение с созданием департамента в id {id}", positionDeleted.posId);
                return;
            }

            var res = await _notificationRepository.AddNotification(resInApp.Value, cancellationToken);

            if (res.IsFailure)
            {
                _logger.LogError("неудалось схранить In-App сообщение с созданием департамента в id {id}", positionDeleted.posId);
                return;
            }


            var resEmail = Notification.CreateEmailPending(
                null,
                "positionDeleted",
                $"Позиция {positionDeleted.name} была удалена",
                "department.delete",
                positionDeleted.posId,
                null,
                DateTime.UtcNow
                );

            if (resInApp.IsFailure)
            {
                _logger.LogError("неудалось создать Email сообщение с удалением позиции в id {id}", positionDeleted.posId);
                return;
            }

            var resDbEmail = await _notificationRepository.AddNotification(resInApp.Value, cancellationToken);

            if (resDbEmail.IsFailure)
            {
                _logger.LogError("неудалось схранить Email сообщение с удалением позиции в id {id}", positionDeleted.posId);
                return;
            }
        }
    }
}
