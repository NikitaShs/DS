using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using PetDS.Domain.Position;
using PetDS.Domain.Position.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Infrastructure.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Domain.Position.Position>
    {
        public void Configure(EntityTypeBuilder<Domain.Position.Position> builder)
        {
            builder.ToTable("positions");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .HasConversion(i => i.ValueId, ValueId => PositionId.Create(ValueId));

            builder.OwnsOne(i => i.Name, n =>
            {
                n.Property(q => q.ValueName).IsRequired();
                n.HasIndex(i => i.ValueName).IsUnique();
            });

            builder.OwnsOne(i => i.Discription, n => n.Property(q => q.ValueDiscription).IsRequired(false));

            builder.Property(i => i.IsActive).HasDefaultValue(true).IsRequired();

            builder.Property(c => c.CreateAt).IsRequired();

            builder.Property(u => u.UpdateAt).IsRequired();

        }
    }
}
