using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetDS.Domain.Location;
using PetDS.Domain.Location.VO;

namespace PetDS.Infrastructure.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).HasConversion(id => id.ValueId, ValueId => LocationId.Create(ValueId));

        builder.OwnsOne(
            i => i.Address, // loc владеет адресом
            a =>
            {
                a.ToJson(); // переводим свойство в json
                a.Property(a => a.City).HasColumnName("city");

                a.Property(a => a.Strit).HasColumnName("street");

                a.Property(a => a.NamberHouse).HasColumnName("houseNumber");
            });

        builder.OwnsOne(i => i.Name, n =>
        {
            n.Property(q => q.ValueName).IsRequired();
            n.HasIndex(i => i.ValueName).IsUnique();
        });

        builder.OwnsOne(i => i.Timezone, n => n.Property(q => q.LanaCode).IsRequired());

        builder.Property(i => i.IsActive).HasDefaultValue(true).IsRequired();

        builder.Property(c => c.CreateAt).IsRequired();

        builder.Property(u => u.UpdateAt).IsRequired();
    }
}