using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location;
using PetDS.Domain.Location.VO;

namespace PetDS.Infrastructure.Configurations;

public class DepartamentLocationConfiguration : IEntityTypeConfiguration<DepartamentLocation>
{
    public void Configure(EntityTypeBuilder<DepartamentLocation> builder)
    {
        builder.ToTable("departamentLocations");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).HasConversion(i => i.ValueId, ValueId => DepartamentLocationId.Create(ValueId));

        builder.Property(i => i.DepartamentId).HasConversion(i => i.ValueId, ValueId => DepartamentId.Create(ValueId));

        builder.Property(i => i.LocationId).HasConversion(i => i.ValueId, ValueId => LocationId.Create(ValueId));


        builder.HasOne<Departament>().WithMany(i => i.DepartamentLocations).HasForeignKey(i => i.DepartamentId);

        builder.HasOne<Location>().WithMany().HasForeignKey(i => i.LocationId).OnDelete(DeleteBehavior.Cascade);
    }
}