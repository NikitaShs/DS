using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotificationService.Domain.Entites;
using Wolverine.EntityFrameworkCore;

namespace NotificationService.Infrastructure.DataBaseConnection
{
    public class NotificationDbContext(string connectionString) : DbContext
    {
        public DbSet<Notification> Notifications => Set<Notification>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactore())
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.MapWolverineEnvelopeStorage("public");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationDbContext).Assembly);
        }


        private ILoggerFactory CreateLoggerFactore() => LoggerFactory.Create(builder => builder.AddConsole());

    }
}
