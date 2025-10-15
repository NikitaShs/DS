using Microsoft.Extensions.DependencyInjection;
using PetDS.Application.Locations;
using PetDS.Application.Locations.CreateLocation;
using PetDS.Infrastructure;
using PetDS.Application;
using System;
using Serilog;
using Serilog.Events;
using PetDS.Application.Departaments;
using PetDS.Application.Departaments.CreateDepartament;
using PetDS.Application.Positions;
using PetDS.Application.Positions.PositionCreate;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("seq") ?? throw new ArgumentNullException("seq"))
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .CreateLogger();
builder.Services.AddSerilog();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IDepartamentRepository, DepartamentRepository>();
builder.Services.AddScoped<LocationCreateService>();
builder.Services.AddScoped<DepartamentCreateServise>();
builder.Services.AddScoped<PositionCreateServise>();
builder.Services.AddScoped<IPositionRepositiry, PositionRepository>();

builder.Services.AddApplication();
var app = builder.Build();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "PetDS"));

    // автоматические миграции

    using var scope = app.Services.CreateAsyncScope();
    var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbcontext.Database.MigrateAsync();
}

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();
