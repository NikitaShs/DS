using Microsoft.Extensions.DependencyInjection;
using PetDS.Application.Locations;
using PetDS.Application.Locations.CreateLocation;
using PetDS.Infrastructure;
using PetDS.Application;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<LocationCreateService>();
builder.Services.AddApplication();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "PetDS"));
}

app.MapControllers();

app.Run();
