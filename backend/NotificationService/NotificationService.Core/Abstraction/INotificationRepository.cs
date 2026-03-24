using CSharpFunctionalExtensions;
using NotificationService.Domain.Entites;
using SharedKernel.Exseption;

namespace NotificationService.Core.Abstraction
{
    public interface INotificationRepository
    {
        Task<Result<Guid, Error>> AddNotification(Notification notification, CancellationToken cancellationToken);

        Task<Result<bool, Error>> CheckDuplicatNotification(string sourceEvent, Guid sourceEntityId, CancellationToken cancellationToken);
    }
}