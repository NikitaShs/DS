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
    public class VideoAsset : MediaAsset
    {
        public const long MAX_SIZE = 5_368_709_120;
        public const string BUCKET = "videos";
        public const string RAW_PREFIX = "raw";
        public const string HLS_PREFIX = "hls";
        public const string MASTER_PLAYLIST_NAME = "master.m3u8";
        public static readonly string[] AllowedExtensions = ["mp4", "mkv", "avi", "mov"];

        private VideoAsset(Guid id, MediaData mediaData, AssetType assetType, StorageKey key, StatusMedia statusMedia, MediaOwner owner)
            : base(id, mediaData, assetType, key, statusMedia, owner) {}

        public static UnitResult<Error> ValidateForUpload(MediaData mediaData)
        {
            if (!AllowedExtensions.Contains(mediaData.FiilName.Extention))
                return GeneralErrors.ValueNotValid("Extention");

            if (mediaData.Size > MAX_SIZE)
                return GeneralErrors.ValueNotValid("Size");

            if (mediaData.ContentType.MediaType != MediaType.VIDEO)
                return GeneralErrors.ValueNotValid("MediaType");

            return UnitResult.Success<Error>();
        }

        public static Result<VideoAsset, Error> CreateForUpload(Guid id, MediaData mediaData, AssetType assetType, StorageKey key, MediaOwner owner)
        {
            if (ValidateForUpload(mediaData).IsFailure)
                return GeneralErrors.ValueNotValid("IsFailure");

            key = StorageKey.Create(id.ToString(), RAW_PREFIX, BUCKET).Value;

            return new VideoAsset(id, mediaData, assetType, key, StatusMedia.UPLOADING, owner);
        }

    }
}
