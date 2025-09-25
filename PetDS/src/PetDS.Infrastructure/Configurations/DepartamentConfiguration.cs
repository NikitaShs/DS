using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Shered;

namespace PetDS.Infrastructure.Configurations
{
    public class DepartamentConfiguration : IEntityTypeConfiguration<Departament>
    {
        public void Configure(EntityTypeBuilder<Departament> builder)
        {
            builder.ToTable("departaments"); // name

            builder.HasKey(i => i.Id); // PK

            builder.Property(i => i.Id).
                HasConversion(
                id => id.ValueId,
                ValueId => DepartamentId.Create(ValueId)); // HasConversion позволяет преоброзовать свойство(VO) сущности в тип для БД и обратно

            builder.OwnsOne(i => i.Name, n =>
            {
                n.Property(q => q.ValueName).IsRequired();
                n.HasIndex(i => i.ValueName).IsUnique();
            });

            builder.OwnsOne(i => i.Identifier, n =>
            n.Property(q => q.ValueIdentifier).HasMaxLength(Constans.MAX_150_lENGHT_DEP).IsRequired());

            builder.OwnsOne(i => i.Path, n => n.Property(q => q.ValuePash).IsRequired());

            builder.Property(d => d.Depth).IsRequired();

            builder.Property(i => i.IsActive).HasDefaultValue(true).IsRequired();

            builder.Property(c => c.CreateAt).IsRequired();

            builder.Property(u => u.UpdateAt).IsRequired();

            builder.HasOne(i => i.Parent)
                .WithMany(i => i.Children).HasForeignKey("parent_Id")
                .IsRequired(false).OnDelete(DeleteBehavior.NoAction);

            // кроме мени мени

        }
    }
}
