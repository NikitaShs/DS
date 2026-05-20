using Core.Adstract;
using Dapper;
using FileService.Contracts.HttpCommunicationFile;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using NotificationService.Web.Extensions;
using PetDS.Application;
using PetDS.Application.abcstractions;
using PetDS.Application.Departaments;
using PetDS.Application.Departaments.Commands.DeleteDepartament;
using PetDS.Application.Departaments.Commands.UpdateDepartament.UpdateDepartamentLocations;
using PetDS.Application.Departaments.CreateDepartament;
using PetDS.Application.Departaments.Queries;
using PetDS.Application.Departaments.UpdateDepartament.UpdateDepartamentDepartamentHierarchy;
using PetDS.Application.Locations;
using PetDS.Application.Locations.CreateLocation;
using PetDS.Application.Locations.Queries;
using PetDS.Application.Messaging;
using PetDS.Application.Positions;
using PetDS.Application.Positions.PositionCreate;
using PetDS.Infrastructure.BackGroundService;
using PetDS.Infrastructure.DataBaseConnections;
using PetDS.Infrastructure.Repositorys;
using PetDS.Infrastructure.Seeding;
using PetDS.Infrastructure.Seedings;
using PetDS.Web;
using PetDS.Web.Middlewares;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Loki;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.AddConfigureSerilogForLoki("notification-service");

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.ConfigAddWolverine(builder.Configuration);
builder.Services.AddApplication();
builder.AddOpenTelemetryMetrics("NotificationService");

builder.Services.AddStackExchangeRedisCache(setup =>
{
    setup.Configuration = builder.Configuration.GetConnectionString("redis");
});
builder.Services.AddHybridCache(options =>
{
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        LocalCacheExpiration = TimeSpan.FromMinutes(1), Expiration = TimeSpan.FromMinutes(30)
    };
});
builder.Services.AddHostedService<DeleteNoActiveDepartamentsBackGroundService>();

builder.Services.AddFilesService(builder.Configuration);
builder.Services.AddScoped<IReadDbContext, ApplicationDbContext>(q =>
    new ApplicationDbContext(builder.Configuration.GetConnectionString("BDDS")));
builder.Services.AddScoped<ApplicationDbContext>(q =>
    new ApplicationDbContext(builder.Configuration.GetConnectionString("BDDS")));
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IDepartamentRepository, DepartamentRepository>();
builder.Services.AddScoped<LocationCreateService>();
builder.Services.AddScoped<DeleteDepartamentServise>();
builder.Services.AddScoped<GetChildsDepartamentsServise>();
builder.Services.AddScoped<GetRootsDepartamentsServise>();
builder.Services.AddScoped<DepartamentCreateServise>();
builder.Services.AddScoped<GetLocationFullServise>();
builder.Services.AddScoped<GetTopFiveDepartamentsServise>();
builder.Services.AddScoped<PositionCreateServise>();
builder.Services.AddScoped<IPositionRepositiry, PositionRepository>();
builder.Services.AddScoped<UpdateDepartamentLocationsServise>();
builder.Services.AddScoped<UpdateDepartamentHierarchyServise>();
builder.Services.AddSingleton<IConnectionFactory, NpgsqlConnectionFactory>();
DefaultTypeMap.MatchNamesWithUnderscores = true; // для устранения ошибки при мапинге из-за имён
builder.Services.AddScoped<IConnectionManeger, ConnectionManeger>();
builder.Services.AddScoped<ISeeding, Seeding>();
builder.Services.AddScoped<GetByIdDepartament>();

WebApplication app = builder.Build();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseCors(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });

    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "PetDS"));

    //автоматические миграции

    using AsyncServiceScope scope = app.Services.CreateAsyncScope();
    ApplicationDbContext dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbcontext.Database.MigrateAsync();

    if (args.Contains("-seeding"))
    {
       await app.Services.GoSeeding();
    }
}

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();


namespace PetDS.Web
{
    public class Program
    {
    }
}