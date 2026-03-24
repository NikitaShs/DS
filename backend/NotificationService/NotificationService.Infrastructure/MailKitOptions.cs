using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure
{
    public record MailKitOptions
    {
        public string Password { get; init; } = string.Empty;

        public string Login { get; init; } = string.Empty;

        public bool UseSsl { get; init; }

        public string AddresServer { get; init; } = string.Empty;

        public int Port { get; init; }

        public long Timeout { get; init; }

        public string FromName { get; set; } = string.Empty;

        public string FromEmail { get; set; } = string.Empty;
    }
}
