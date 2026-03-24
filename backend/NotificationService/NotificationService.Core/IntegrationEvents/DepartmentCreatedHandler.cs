using Core.MessagingComminication.MessagingDto.Departament;
using Microsoft.Extensions.Logging;
using NotificationService.Core.Abstraction;
using NotificationService.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Core.IntegrationEvents
{
    public sealed class DepartmentCreatedHandler
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<DepartmentCreatedHandler> _logger;

        public DepartmentCreatedHandler(INotificationRepository notificationRepository, ILogger<DepartmentCreatedHandler> logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        public async Task Handle(DepartamentCreate departamentCreate, CancellationToken cancellationToken)
        {
            var resCheck = await _notificationRepository.CheckDuplicatNotification("department.created", departamentCreate.depthId, cancellationToken);
            if (resCheck.IsFailure)
            {
                _logger.LogError("да такое смс уже было");
                return;
            }


            var resInApp = Notification.CreateInApp(
                null,
                "DepartamentCreate",
                $"Создано новое подразделение: {departamentCreate.name},- {departamentCreate.identifier}",
                "department.created",
                departamentCreate.depthId,
                null,
                DateTime.UtcNow
                );

            if (resInApp.IsFailure)
            {
                _logger.LogError("неудалось создать In-App сообщение с созданием департамента в id {id}", departamentCreate.depthId);
                return;
            }

            var res = await _notificationRepository.AddNotification(resInApp.Value, cancellationToken);

            if(res.IsFailure)
            {
                _logger.LogError("неудалось схранить In-App сообщение с созданием департамента в id {id}", departamentCreate.depthId);
                return;
            }    

        }
    }
}
