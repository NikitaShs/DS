using Microsoft.EntityFrameworkCore;
using PetDS.Application;
using PetDS.Application.abcstractions;
using PetDS.Application.Departaments;
using PetDS.Application.Departaments.Commands.UpdateDepartament.UpdateDepartamentLocations;
using PetDS.Application.Departaments.CreateDepartament;
using PetDS.Application.Departaments.Queries;
using PetDS.Application.Departaments.UpdateDepartament.UpdateDepartamentDepartamentHierarchy;
using PetDS.Application.Locations;
using PetDS.Application.Locations.CreateLocation;
using PetDS.Application.Positions;
using PetDS.Application.Positions.PositionCreate;
using PetDS.Infrastructure.DataBaseConnections;
using PetDS.Infrastructure.Repositorys;
using PetDS.Infrastructure.Seeding;
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
builder.Services.AddScoped<IReadDbContext, ApplicationDbContext>();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IDepartamentRepository, DepartamentRepository>();
builder.Services.AddScoped<LocationCreateService>();
builder.Services.AddScoped<DepartamentCreateServise>();
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