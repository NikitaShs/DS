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

            builder.Property(q => q.UpdateAd).IsRequired();

            builder.Property(q => q.CreateAt).IsRequired();

            builder.Property(q => q.IsActive).IsRequired();

            builder.Property(q => q.DeletedAt);

            builder.OwnsOne(q => q.MediaData, m =>
            {
                m.ToJson();
                m.Property(m => m.Size).IsRequired().HasColumnName("size");
                m.Property(m => m.ExpectedChunksCount).IsRequired().HasColumnName("expected_chunks_count");
                m.OwnsOne(m => m.FileName, f =>
                {
                    //f.ToJson();
                    f.Property(f => f.ValueName).IsRequired().HasColumnName("name");
                    f.Property(f => f.Extention).IsRequired().HasColumnName("extension");
                });
                m.OwnsOne(m => m.ContentType, c =>
                {
                    //c.ToJson();
                    c.Property(f => f.ValueContentType).IsRequired().HasColumnName("value_content_type");
                    c.Property(f => f.MediaType).HasConversion<string>().IsRequired().HasColumnName("media_type");
                });
            });

            builder.Property(q => q.AssetType).HasConversion<string>().IsRequired();

            builder.Property(q => q.StatusMedia).HasConversion<string>().IsRequired();

            builder.OwnsOne(q => q.StorageKey, k =>
            {
                k.ToJson();
                k.Property(f => f.Bucket).IsRequired().HasColumnName("bucket");
                k.Property(f => f.Prefix).IsRequired(false).HasColumnName("prefix");
                k.Property(f => f.ValueKey).IsRequired().HasColumnName("value_key");
                k.Property(f => f.ValuePreKey).IsRequired().HasColumnName("value_prekey");
                k.Property(f => f.FullPath).IsRequired().HasColumnName("full_path");
            });

            builder.OwnsOne(q => q.Owner, o =>
            {
                o.ToJson();
                o.Property(f => f.Context).IsRequired().HasColumnName("context");
                o.Property(f => f.EntiteId).IsRequired().HasColumnName("entity_id");
            });


        }
    }
}
