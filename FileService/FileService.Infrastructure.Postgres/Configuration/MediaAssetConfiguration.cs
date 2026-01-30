using FileService.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.Postgres.Configuration
{
    public class MediaAssetConfiguration : IEntityTypeConfiguration<MediaAsset>
    {
        public void Configure(EntityTypeBuilder<MediaAsset> builder)
        {
            builder.ToTable("MediaAsset");

            builder.HasDiscriminator<string>("asset_type").HasValue<VideoAsset>("VideoAsset").HasValue<PreviewAsset>("PreviewAsset");

            builder.HasKey(q => q.Id);

            builder.Property(q => q.UpdateAd);

            builder.Property(q => q.CreateAt);

            builder.OwnsOne(q => q.MediaData, m =>
            {
                m.ToJson();
                m.Property(m => m.Size);
                m.Property(m => m.ExpectedChunksCount);
                m.OwnsOne(m => m.FiilName, f =>
                {
                    f.ToJson();
                    f.Property(f => f.ValueName);
                    f.Property(f => f.Extention);
                });
                m.OwnsOne(m => m.ContentType, c =>
                {
                    c.ToJson();
                    c.Property(f => f.ValueContentType);
                    c.Property(f => f.MediaType).HasConversion<string>();
                });
            });

            builder.Property(q => q.AssetType).HasConversion<string>();

            builder.Property(q => q.StatusMedia).HasConversion<string>();

            builder.OwnsOne(q => q.Key, k =>
            {
                k.ToJson();
                k.Property(f => f.Bucket);
                k.Property(f => f.Prefix);
                k.Property(f => f.Key);
                k.Property(f => f.Value);
                k.Property(f => f.FullPath);
            });

            builder.OwnsOne(q => q.Owner, o =>
            {
                o.ToJson();
                o.Property(f => f.Context);
                o.Property(f => f.EntiteId);
            });


        }
    }
}
