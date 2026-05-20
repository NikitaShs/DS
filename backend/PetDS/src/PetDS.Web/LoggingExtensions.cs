using Serilog;

namespace NotificationService.Web.Extensions;

public static class LoggingExtensions
{
    public static IHostBuilder AddConfigureSerilogForLoki(this IHostBuilder host, string serviceName = "notification-service")
    {
        host.UseSerilog((context, services, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
            config.ReadFrom.Services(services);

            config.Enrich.FromLogContext()
                  .Enrich.WithMachineName()
                  .Enrich.WithThreadId()
                  .Enrich.WithProcessId()
                  .Enrich.WithProperty("service", serviceName);
        });

        return host;
    }
}