using CSharpFunctionalExtensions;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain.Entites
{
    public class Notification
    {
        private Notification() { }

        private Notification(
            Guid? resipientsId,
            ChannelType channel,
            string subject,
            string body,
            string sourseEvent,
            StatusType statusType,
            Guid? sourseEntityId,
            string? error,
            DateTime? sentAt
            )
        {
            Id = Guid.NewGuid();
            ResipientId = resipientsId;
            Channel = channel;
            Subject = subject;
            Body = body;
            Status = statusType;
            SourseEvent = sourseEvent;
            SourseEntityId = sourseEntityId;
            CreateAt = DateTime.UtcNow;
            IsRead = false;

        }
        public Guid Id { get; private set; }

        public Guid? ResipientId { get; private set; }

        public ChannelType Channel { get; private set; }

        public string Subject { get; private set; }

        public string Body { get; private set; }

        public StatusType Status { get; private set; }

        public string SourseEvent { get; private set; }

        public Guid? SourseEntityId { get; private set; }

        public DateTime CreateAt { get; private set; }

        public DateTime? SentAt { get; private set; }

        public string? Error { get; private set; }

        public bool? IsRead { get; private set; }


        public static Result<Notification, Error> CreateEmailPending(
            Guid? resipientsId,
            string subject,
            string body,
            string sourseEvent,
            Guid? sourseEntityId,
            string? error,
            DateTime? sentAt)
        {
            var res = ValidParams(subject, body, sourseEvent);
            if (res.IsFailure)
                return res.Error;

            return new Notification(resipientsId, ChannelType.Email, subject, body, sourseEvent, StatusType.Pending, sourseEntityId, error, sentAt);
        }

        public static Result<Notification, Error> CreateInApp(
            Guid? resipientsId,
            string subject,
            string body,
            string sourseEvent,
            Guid? sourseEntityId,
            string? error,
            DateTime? sentAt)
        {
            var res = ValidParams(subject, body, sourseEvent);
            if (res.IsFailure)
                return res.Error;

            return new Notification(resipientsId, ChannelType.InApp, subject, body, sourseEvent, StatusType.Send, sourseEntityId, error, sentAt);
        }


        private static Result<bool, Error> ValidParams(
            string subject,
            string body,
            string sourseEvent)
        {
            if(string.IsNullOrWhiteSpace(subject) || subject.Length >= 50)
                return GeneralErrors.ValueNotValid(subject);
            if (string.IsNullOrWhiteSpace(body) || body.Length >= 500)
                return GeneralErrors.ValueNotValid(body);
            if (string.IsNullOrWhiteSpace(sourseEvent) || sourseEvent.Length >= 20)
                return GeneralErrors.ValueNotValid(sourseEvent);
            return true;
        }

        public void UpdateStatusToSend()
        {
            Status = StatusType.Send;
        }


    }

    public enum ChannelType
    {
        Email,
        InApp
    }

    public enum StatusType
    {
        Pending,
        Send,
        Failed
    }

    

}
