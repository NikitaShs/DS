using CSharpFunctionalExtensions;
using MailKit.Net.Smtp;
using Microsoft.Build.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error = SharedKernel.Exseption.Error;

namespace NotificationService.Infrastructure.Repository
{
    public class EmailSender
    {
        private readonly SmtpClient _smtpClient;
        private readonly MailKitOptions _mailKitOptions;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(SmtpClient smtpClient, IOptions<MailKitOptions> mailKitOptions, ILogger<EmailSender> logger)
        {
            _smtpClient = smtpClient;
            _mailKitOptions = mailKitOptions.Value;
            _logger = logger;
        }

        public async Task<Result<bool, Error>> SendAsync(string to, string subject, string body, CancellationToken cancellationToken)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_mailKitOptions.FromName, _mailKitOptions.FromEmail));

                message.To.Add(new MailboxAddress("Recipient", "nisc2008faiv@gmail.com"));

                message.Subject = subject;

                message.Body = new TextPart("plain") { Text = body };

                await _smtpClient.SendAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("неудалось отправить сообщение ошибка {ex}", ex);
                return GeneralErrors.Unknown();

            }
            

        }
    }
}
