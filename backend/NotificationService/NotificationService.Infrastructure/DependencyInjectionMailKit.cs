using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure
{
    public static class DependencyInjectionMailKit
    {
        public static IServiceCollection AddMailKit(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<MailKitOptions>(configuration.GetSection(nameof(MailKitOptions)));

            services.AddSingleton<SmtpClient>(q =>
            {
                var client = new SmtpClient();

                var option = q.GetRequiredService<IOptions<MailKitOptions>>().Value;

                client.Connect(
                    option.AddresServer,  // Адрес SMTP сервера
                    option.Port,               // Порт (587 для TLS, 465 для SSL)
                    option.UseSsl               // Использовать SSL/TLS
                );

                //client.Authenticate(
                //    option.Login,
                //    option.Password
                //);


                client.Timeout = 10000;

                return client;
            });


            return services;
        }
    }
}
