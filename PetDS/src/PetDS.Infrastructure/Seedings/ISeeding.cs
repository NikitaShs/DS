namespace PetDS.Infrastructure.Seeding;

public interface ISeeding
{
    Task<SeedingResult> SeedAsync();
}