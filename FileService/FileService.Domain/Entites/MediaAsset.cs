using FileService.Domain.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Domain.Entites
{
    public abstract class MediaAsset
    {
        protected MediaAsset() { }

        public Guid Id { get; protected set; }

        public MediaData MediaData { get; protected set; }

        public AssetType AssetType { get; protected set; }

        public DateTime CreateAt { get; protected set; } = DateTime.UtcNow;

        public DateTime UpdateAd { get; protected set; } = DateTime.UtcNow;

        public StorageKey Key { get; protected set; }

        public StatusMedia StatusMedia { get; protected set; }

        public MediaOwner Owner { get; protected set; }

        protected MediaAsset(Guid id, MediaData mediaData, AssetType assetType, StorageKey key, StatusMedia statusMedia, MediaOwner owner)
        {
            Id = id;
            MediaData = mediaData;
            AssetType = assetType;
            Key = key;
            StatusMedia = statusMedia;
            Owner = owner;
            CreateAt = DateTime.UtcNow;
            UpdateAd = DateTime.UtcNow;
        }
    }

}
