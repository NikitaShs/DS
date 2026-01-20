using Core.adstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
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
using PetDS.Application.Positions;
using PetDS.Application.Positions.PositionCreate;
using PetDS.Infrastructure.BackGroundService;
using PetDS.Infrastructure.DataBaseConnections;
using PetDS.Infrastructure.Repositorys;
using PetDS.Infrastructure.Seeding;
using PetDS.Infrastructure.Seedings;
using PetDS.Web.Middlewares;
using Serilog;
using Serilog.Events;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddApplication();
builder.Services.AddStackExchangeRedisCache(setup =>
{
    setup.Configuration = builder.Configuration.GetConnectionString("redis");
});
builder.Services.AddHybridCache(options =>
{
    options.DefaultEntryOptions = new HybridCacheEntryOptions()
    {
        LocalCacheExpiration = TimeSpan.FromMinutes(1), Expiration = TimeSpan.FromMinutes(30)
    };
});
builder.Services.AddHostedService<DeleteNoActiveDepartamentsBackGroundService>();
builder.Services.AddScoped<IReadDbContext, ApplicationDbContext>(q => new ApplicationDbContext(builder.Configuration.GetConnectionString("BDDS")));
builder.Services.AddScoped<ApplicationDbContext>(q => new ApplicationDbContext(builder.Configuration.GetConnectionString("BDDS")));
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
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true; // для устранения ошибки при мапинге из-за имён
builder.Services.AddScoped<IConnectionManeger, ConnectionManeger>();
builder.Services.AddScoped<ISeeding, Seeding>();
builder.Services.AddScoped<GetByIdDepartament>();

WebApplication app = builder.Build();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "PetDS"));

    // автоматические миграции

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
    public partial class Program { }
}