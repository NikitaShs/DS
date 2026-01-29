using FileService.Infrastructure.Postgres.DataBase;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddSerilog();
builder.Services.AddScoped<PostgresDbContext>(q => new PostgresDbContext(builder.Configuration.GetConnectionString("BDFS")));
builder.Services.AddControllers();
builder.Services.AddOpenApi();

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

app.MapControllers();

app.Run();
