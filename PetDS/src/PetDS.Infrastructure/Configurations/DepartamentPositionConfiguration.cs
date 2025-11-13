using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Position;
using PetDS.Domain.Position.VO;

namespace PetDS.Infrastructure.Configurations;

public class DepartamentPositionConfiguration : IEntityTypeConfiguration<DepartamentPosition>
{
    public void Configure(EntityTypeBuilder<DepartamentPosition> builder)
    {
        builder.ToTable("departamentPositions");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).HasConversion(i => i.ValueId, ValueId => DepartamentPositionId.Create(ValueId));


        builder.Property(i => i.DepartamentId).HasConversion(i => i.ValueId, ValueId => DepartamentId.Create(ValueId));

        builder.Property(i => i.PositionId).HasConversion(i => i.ValueId, ValueId => PositionId.Create(ValueId));

        builder.HasOne<Departament>().WithMany(i => i.DepartamentPositions).HasForeignKey(i => i.DepartamentId);

        builder.HasOne<Position>().WithMany(i => i.departamentPositions).HasForeignKey(i => i.PositionId);
    }
}