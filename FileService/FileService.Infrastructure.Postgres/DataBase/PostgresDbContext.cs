using FileService.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.Postgres.DataBase
{
    public class PostgresDbContext(string connectionString) : DbContext
    {

        public DbSet<PreviewAsset> PreviewAssets => Set<PreviewAsset>();

        public DbSet<VideoAsset> VideoAsset => Set<VideoAsset>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory()).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgresDbContext).Assembly);
        }

        public ILoggerFactory CreateLoggerFactory() => LoggerFactory.Create(b => b.AddConsole());
    }
}
