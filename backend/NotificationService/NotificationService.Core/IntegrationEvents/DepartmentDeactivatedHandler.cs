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
    public sealed class DepartmentDeactivatedHandler
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<DepartmentDeactivatedHandler> _logger;

        public DepartmentDeactivatedHandler(INotificationRepository notificationRepository, ILogger<DepartmentDeactivatedHandler> logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        public async Task Handle(DepartmentDeactivated departmentDeactivated, CancellationToken cancellationToken)
        {
            var resCheck = await _notificationRepository.CheckDuplicatNotification("department.created", departmentDeactivated.depthIs, cancellationToken);
            if (resCheck.IsFailure)
            {
                _logger.LogError("да такое смс уже было");
                return;
            }

            var resInApp = Notification.CreateInApp(
                null,
                "DepartamentCreate",
                $"Подразделение {departmentDeactivated.name} было деактивировано",
                "department.deactivated",
                departmentDeactivated.depthIs,
                null,
                DateTime.UtcNow
                );

            if (resInApp.IsFailure)
            {
                _logger.LogError("неудалось создать In-App сообщение с деактивацией департамента в id {id}", departmentDeactivated.depthIs);
                return;
            }

            var res = await _notificationRepository.AddNotification(resInApp.Value, cancellationToken);

            if (res.IsFailure)
            {
                _logger.LogError("неудалось схранить In-App сообщение с деактивацией департамента в id {id}", departmentDeactivated.depthIs);
                return;
            }


            var resEmail = Notification.CreateEmailPending(
                null,
                "DepartamentCreate",
                $"Подразделение {departmentDeactivated.name} было деактивировано",
                "department.deactivated",
                departmentDeactivated.depthIs,
                null,
                DateTime.UtcNow
                );

            if (resInApp.IsFailure)
            {
                _logger.LogError("неудалось создать Email сообщение с деактивацией департамента в id {id}", departmentDeactivated.depthIs);
                return;
            }

            var resDbEmail = await _notificationRepository.AddNotification(resInApp.Value, cancellationToken);

            if (resDbEmail.IsFailure)
            {
                _logger.LogError("неудалось схранить Email сообщение с деактивацией департамента в id {id}", departmentDeactivated.depthIs);
                return;
            }

        }

    }
}
