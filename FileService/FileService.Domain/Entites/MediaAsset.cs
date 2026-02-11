using CSharpFunctionalExtensions;
using FileService.Domain.VO;
using SharedKernel.Exseption;
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

        public StorageKey StorageKey { get; protected set; }

        public bool IsActive { get; private set; } = true;

        public DateTime? DeletedAt { get; private set; } = null;

        public StatusMedia StatusMedia { get; protected set; }

        public MediaOwner Owner { get; protected set; }

        protected MediaAsset(Guid id, MediaData mediaData, AssetType assetType, StorageKey storageKey, StatusMedia statusMedia, MediaOwner owner)
        {
            Id = id;
            MediaData = mediaData;
            AssetType = assetType;
            StorageKey = storageKey;
            StatusMedia = statusMedia;
            Owner = owner;
            CreateAt = DateTime.UtcNow;
            UpdateAd = DateTime.UtcNow;
        }

        public void UpdateStatusToUpoaded()
        {
            StatusMedia = StatusMedia.UPLOADED;
        }

        public void UpdateStatusToFail()
        {
            StatusMedia = StatusMedia.FAILED;
            IsActive = false;
            DeletedAt = DateTime.UtcNow.AddDays(1);
        }

        public static Result<MediaAsset, Errors> CreateTypedMediaAsset(AssetType assetType, MediaData mediaData, MediaOwner mediaOvner)
        {
            switch (assetType)
            {
                case AssetType.VIDEO:
                    var videoAsset = VideoAsset.CreateForUpload(Guid.NewGuid(), mediaData, AssetType.VIDEO, mediaOvner);
                    if (videoAsset.IsFailure)
                        return GeneralErrors.ValueFailure("VideoAsset").ToErrors();
                    return videoAsset.Value;
                    break;
                case AssetType.PREVIEW:
                    var previewAsset = PreviewAsset.CreateForUpload(Guid.NewGuid(), mediaData, AssetType.VIDEO, mediaOvner);
                    if (previewAsset.IsFailure)
                        return GeneralErrors.ValueFailure("PreviewAsset").ToErrors();
                    return previewAsset.Value;
                    break;
                case AssetType.AVATAR:
                    return GeneralErrors.Unknown().ToErrors();
                    break;
                case AssetType.IMAGE:
                    return GeneralErrors.Unknown().ToErrors();
                    break;
                default:
                    return GeneralErrors.Unknown().ToErrors();
            }
        }

    }

}
