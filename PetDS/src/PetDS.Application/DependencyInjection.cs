using Microsoft.Extensions.DependencyInjection;
using PetDS.Application.abcstractions;
using PetDS.Application.Locations.CreateLocation;

namespace PetDS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IHandler<Guid, CreateLocationCommand>, LocationCreateService>();

            return services;
        }
    }
}
