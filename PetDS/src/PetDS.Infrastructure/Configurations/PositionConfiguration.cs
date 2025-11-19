using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetDS.Domain.Position;
using PetDS.Domain.Position.VO;

namespace PetDS.Infrastructure.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .HasConversion(i => i.ValueId, ValueId => PositionId.Create(ValueId));

        builder.OwnsOne(i => i.Name, n =>
        {
            n.Property(q => q.ValueName).IsRequired().HasColumnName("name");
            n.HasIndex(i => i.ValueName).IsUnique();
        });

        builder.OwnsOne(i => i.Discription, n => 
            n.Property(q => q.ValueDiscription).IsRequired(false).HasColumnName("discription"));

        builder.Property(i => i.IsActive).HasDefaultValue(true).IsRequired();

        builder.Property(c => c.CreateAt).IsRequired();

        builder.Property(u => u.UpdateAt).IsRequired();
    }
}