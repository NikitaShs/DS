using CSharpFunctionalExtensions;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Core.Abstraction
{
    public interface IEmailSender
    {
        Task<Result<bool, Error>> SendAsync(string to, string subject, string body, CancellationToken cancellationToken);
    }
}
