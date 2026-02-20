using FileService.Core;
using FileService.Core.abstractions;
using FileService.Infrastructure.Postgres.DataBase;
using FileService.Infrastructure.Postgres.Repositori;
using FileService.Infrastructure.S3;
using FileService.Infrastructure.S3.BackgroundServise;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("seq") ?? throw new ArgumentNullException("seq"))
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .CreateLogger();

builder.Services.AddEndpoints();
builder.Services.AddSerilog();
builder.Services.AddS3(builder.Configuration);
builder.Services.AddScoped<IS3Provider, S3Provider>();
builder.Services.AddScoped<IMediaRepository, MediaRepository>();
builder.Services.AddScoped<PostgresDbContext>(q => new PostgresDbContext(builder.Configuration.GetConnectionString("BDFS")));
builder.Services.AddCore();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHostedService<S3BucketInitializationServise>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "FileServise"));
    app.MapOpenApi();

    using var scope = app.Services.CreateAsyncScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<PostgresDbContext>();

    await dbContext.Database.MigrateAsync();
}

app.UseSerilogRequestLogging();

app.MapEndpoints();
app.MapControllers();

app.Run();


public partial class Program;