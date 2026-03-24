using Core.MessagingComminication.MessagingDto.Departament;
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
    public sealed class DepartmentLocationsUpdatedHandler
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<DepartmentLocationsUpdatedHandler> _logger;

        public DepartmentLocationsUpdatedHandler(INotificationRepository notificationRepository, ILogger<DepartmentLocationsUpdatedHandler> logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        public async Task Handle(DepartmentLocationsUpdated departmentLocationsUpdated, CancellationToken cancellationToken)
        {
            var resCheck = await _notificationRepository.CheckDuplicatNotification("department.created", departmentLocationsUpdated.depthId, cancellationToken);
            if (resCheck.IsFailure)
            {
                _logger.LogError("да такое смс уже было");
                return;
            }

            var resInApp = Notification.CreateInApp(
                null,
                "departmentLocationsUpdated",
                $"Локации подразделения {departmentLocationsUpdated.name} обновлены",
                "department.locationsUpdated",
                departmentLocationsUpdated.depthId,
                null,
                DateTime.UtcNow
                );

            if (resInApp.IsFailure)
            {
                _logger.LogError("неудалось создать In-App сообщение с обновлением локации департамента в id {id}", departmentLocationsUpdated.depthId);
                return;
            }

            var res = await _notificationRepository.AddNotification(resInApp.Value, cancellationToken);

            if (res.IsFailure)
            {
                _logger.LogError("неудалось схранить In-App сообщение с обновлением локации департамента в id {id}", departmentLocationsUpdated.depthId);
                return;
            }

        }
    }
}
