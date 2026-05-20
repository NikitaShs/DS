using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace PetDS.Web;

/// <summary>
/// Общая конфигурация OpenTelemetry для всех микросервисов.
/// Пока подключаем только метрики; трейсы добавим отдельным методом, когда будут нужны.
/// </summary>
public static class OpenTelemetryExtensions
{
    /// <summary>
    /// Регистрирует OpenTelemetry metrics pipeline:
    /// - Resource: service.name, service.version, service.instance.id, deployment.environment
    ///   (одинаковые лейблы для метрик и логов — общий фильтр в Grafana).
    /// - Auto-instrumentation: HTTP server (ASP.NET Core), HTTP client, .NET runtime (GC, threadpool, exceptions).
    /// - Экспорт по OTLP в коллектор. Endpoint берётся из env-переменной
    ///   OTEL_EXPORTER_OTLP_ENDPOINT (SDK читает её сам); дефолт = http://localhost:4317.
    /// </summary>
    public static WebApplicationBuilder AddOpenTelemetryMetrics(
        this WebApplicationBuilder builder,
        string serviceName)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentException.ThrowIfNullOrWhiteSpace(serviceName);

        string serviceVersion = typeof(OpenTelemetryExtensions).Assembly.GetName().Version?.ToString() ?? "unknown";
        string environmentName = builder.Environment.EnvironmentName;

        builder.Services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(
                    serviceName: serviceName,
                    serviceVersion: serviceVersion,
                    serviceInstanceId: Environment.MachineName)
                .AddAttributes(
                [
                    // deployment.environment — Development / Docker / Production.
                    // В Prometheus станет лейблом deployment_environment.
                    new KeyValuePair<string, object>("deployment.environment", environmentName),
                ]))
            .WithMetrics(metrics => metrics
                // HTTP server: http.server.request.duration, http.server.active_requests
                // плюс Kestrel connections, Routing, Diagnostics, RateLimiting, Authentication (.NET 10+).
                .AddAspNetCoreInstrumentation()
                // HTTP client: http.client.request.duration — исходящие вызовы (HttpClient).
                .AddHttpClientInstrumentation()
                // Runtime: GC, threadpool, исключения, JIT, lock contention, working set.
                .AddRuntimeInstrumentation()
                // Postgres-драйвер: пул коннектов (db.client.connection.count/max),
                // длительность команд (db.client.operation.duration), bytes_read/written,
                // prepared_ratio. Meter встроен в Npgsql 8+, отдельный NuGet не нужен.
                // Если сервис не использует Postgres — просто ничего не публикуется.
                .AddMeter("Npgsql")
                // EF Core: Active DbContexts, Queries (count/rate), SaveChanges,
                // Query Cache Hit Rate, ExecutionStrategy/OptimisticConcurrency failures.
                // Meter встроен в EF Core, отдельный NuGet не нужен.
                .AddMeter("Microsoft.EntityFrameworkCore")
                // Endpoint и протокол читаются из OTEL_EXPORTER_OTLP_ENDPOINT и
                // OTEL_EXPORTER_OTLP_PROTOCOL (default = grpc на 4317).
                .AddOtlpExporter());

        return builder;
    }
}