using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Domain.Departament;
using PetDS.Domain.Location;
using PetDS.Domain.Position;

namespace PetDS.Infrastructure.DataBaseConnections;

public class ApplicationDbContext(string connectionString) : DbContext, IReadDbContext
{
    public DbSet<Departament> Departaments => Set<Departament>();

    public DbSet<Location> Locations => Set<Location>();

    public DbSet<DepartamentLocation> DepartamentLocations => Set<DepartamentLocation>();

    public DbSet<DepartamentPosition> DepartamentPositions => Set<DepartamentPosition>();

    public DbSet<Position> Positions => Set<Position>();

    public IQueryable<Position> ReadPosition => Set<Position>().AsQueryable().AsNoTracking();

    public IQueryable<Location> ReadLocation => Set<Location>().AsQueryable().AsNoTracking();

    public IQueryable<Departament> ReadDepartament => Set<Departament>().AsQueryable().AsNoTracking();

    public IQueryable<DepartamentLocation> ReadDepartamentLocation =>
        Set<DepartamentLocation>().AsQueryable().AsNoTracking();

    public IQueryable<DepartamentPosition> ReadDepartamentPosition =>
        Set<DepartamentPosition>().AsQueryable().AsNoTracking();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactore())
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("ltree");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    private ILoggerFactory CreateLoggerFactore() => LoggerFactory.Create(builder => builder.AddConsole());
}