using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotificationService.Core.Abstraction;
using NotificationService.Domain.Entites;
using NotificationService.Infrastructure.DataBaseConnection;
using NotificationService.Infrastructure.Repository;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.BackGroundService
{
    public class SendMeilBackGroundService : BackgroundService
    {
        private readonly ILogger<SendMeilBackGroundService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public SendMeilBackGroundService(ILogger<SendMeilBackGroundService> logger,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("bacgraung start");
            using IServiceScope scope = _scopeFactory.CreateScope();

            IEmailSender emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();


            NotificationDbContext dbContext = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();

            PeriodicTimer timer = new(TimeSpan.FromMinutes(10));

            _logger.LogInformation("bacgraung inicianalication");


            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                _logger.LogInformation("full start");

                var funcRebublic = await dbContext.Notifications
                    .Where(q => q.Status == StatusType.Pending && q.Channel == ChannelType.Email)
                    .OrderBy(q => q.CreateAt)
                    .Take(150)
                    .ToListAsync(cancellationToken);


                int shit = 0;

                foreach (var q in funcRebublic)
                {
                    shit = 0;
                    while (shit < 3)
                    {

                        _logger.LogInformation("{q}", q.Id);
                        var pulRes = await emailSender.SendAsync(
                        "noreply@notification.local",
                        q.Subject,
                        q.Body,
                        cancellationToken);

                        if (pulRes.IsSuccess)
                        {
                            q.UpdateStatusToSend();

                            _logger.LogInformation("email send to complite");

                            break;
                        }
                        else
                        {
                            if(shit >= 3)
                            {
                                q.UpdateStatusToFailed();

                                break;
                            }
                            shit++;
                            _logger.LogError("email send to not complite {id}", q.Id);
                        }
                    }
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}
