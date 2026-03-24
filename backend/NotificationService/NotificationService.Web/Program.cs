using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using NotificationService.Core.Abstraction;
using NotificationService.Infrastructure.DataBaseConnection;
using NotificationService.Infrastructure.Repository;
using PetDS.Application.Messaging;
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

builder.Services.AddScoped<NotificationDbContext>(q => new NotificationDbContext(builder.Configuration.GetConnectionString("BDNS")));
builder.Services.ConfigAddWolverine(builder.Configuration);
builder.Services.AddSerilog();

SmtpClient

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "NotificationServise"));

    app.MapOpenApi();

    using var scope = app.Services.CreateScope();

    var db = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();

    await db.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
