using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetDS.Domain.Departament;
using PetDS.Domain.Location;
using PetDS.Domain.Position;

namespace PetDS.Infrastructure.DataBaseConnections
{
    public class ApplicationDbContext(IConfiguration configuration) : DbContext
    {
        public DbSet<Departament> Departaments => Set<Departament>();

        public DbSet<Location> Locations => Set<Location>();

        public DbSet<Position> Positions => Set<Position>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("BDDS"));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactore())
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        private ILoggerFactory CreateLoggerFactore() => LoggerFactory.Create(builder => builder.AddConsole());

    }
}
