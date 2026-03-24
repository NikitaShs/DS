using Amazon.Runtime.Internal.Util;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotificationService.Core.Abstraction;
using NotificationService.Domain.Entites;
using NotificationService.Infrastructure.DataBaseConnection;
using SharedKernel.Exseption;

namespace NotificationService.Infrastructure.Repository
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly NotificationDbContext _notificationDbContext;
        private readonly ILogger<NotificationRepository> _logger;

        public NotificationRepository(NotificationDbContext notificationDbContext, ILogger<NotificationRepository> logger)
        {
            _notificationDbContext = notificationDbContext;
            _logger = logger;
        }


        public async Task<Result<Guid, Error>> AddNotification(Notification notification, CancellationToken cancellationToken)
        {
            await _notificationDbContext.Notifications.AddAsync(notification, cancellationToken);

            try
            {
                await _notificationDbContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("успешное сохранение сообщения в БД {notification}", notification.Subject);
                return notification.Id;

            }
            catch (Exception ex)
            {
                _logger.LogInformation("ошибка при сохранении сообщения в БД {notification}", notification.Subject);
                return GeneralErrors.ValueFailure("notification");
            }

        }

        public async Task<Result<bool, Error>> CheckDuplicatNotification(string sourceEvent, Guid sourceEntityId, CancellationToken cancellationToken)
        {
            var res = await _notificationDbContext.Notifications.Where(q => q.SourseEvent == sourceEvent & q.SourseEntityId == sourceEntityId).ToListAsync(cancellationToken);

            if (res.Count > 0)
            {
                _logger.LogInformation("сообщение с такими sourceEvent - {sourceEvent} и sourceEntityId - {sourceEntityId} уже есть", sourceEvent, sourceEntityId);
                return GeneralErrors.ValueNotValid("sourceEvent, sourceEntityId");
            }
            return true;
        }

    }
}
