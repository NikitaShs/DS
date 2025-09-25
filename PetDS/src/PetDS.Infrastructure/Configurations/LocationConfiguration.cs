using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging.Abstractions;
using PetDS.Domain.Location;
using PetDS.Domain.Location.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
