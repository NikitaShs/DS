using Microsoft.Extensions.DependencyInjection;

namespace PetDS.Infrastructure.Seeding;

public static class SeedindExtentions
{
    public static async Task<IServiceProvider> GoSeeding(this IServiceProvider serviceProvider)
    {
        using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();

        IEnumerable<ISeeding> seeding = scope.ServiceProvider.GetServices<ISeeding>();

        foreach (ISeeding seed in seeding)
        {
            await seed.SeedAsync();
        }

        return serviceProvider;
    }
}