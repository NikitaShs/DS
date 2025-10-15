using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetDS.Application.abcstractions;
using PetDS.Application.Departaments.CreateDepartament;
using PetDS.Application.Locations.CreateLocation;
using PetDS.Application.Positions.PositionCreate;

namespace PetDS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IHandler<Guid, CreateLocationCommand>, LocationCreateService>();

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddScoped<IHandler<Guid, CreateDepartamentCommand>, DepartamentCreateServise>();

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddScoped<IHandler<Guid, PositionCreateCommand>, PositionCreateServise>();

            return services;
        }
    }
}
