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
    public class PreviewAsset : MediaAsset
    {
        public const long MAX_SIZE = 10_485_760;
        public const string BUCKET = "preview";
        public const string RAW_PREFIX = "raw";
        public static readonly string[] AllowedExtensions = ["jpg", "jpeg", "png", "webp"];

        private PreviewAsset() { }

        private PreviewAsset(Guid id, MediaData mediaData, AssetType assetType, StorageKey StorageKey, StatusMedia statusMedia, MediaOwner owner)
            : base(id, mediaData, assetType, StorageKey, statusMedia, owner) { }


        public static UnitResult<Error> ValidateForUpload(MediaData mediaData)
        {
            if (!AllowedExtensions.Contains(mediaData.FileName.Extention))
                return GeneralErrors.ValueNotValid("Extention");

            if (mediaData.Size > MAX_SIZE)
                return GeneralErrors.ValueNotValid("Size");

            if (mediaData.ContentType.MediaType != MediaType.IMAGE)
                return GeneralErrors.ValueNotValid("MediaType");

            return UnitResult.Success<Error>();
        }

        public static Result<PreviewAsset, Error> CreateForUpload(Guid id, MediaData mediaData, AssetType assetType, MediaOwner owner)
        {
            if (ValidateForUpload(mediaData).IsFailure)
                return GeneralErrors.ValueNotValid("IsFailure");

            var key = StorageKey.Create(id.ToString(), RAW_PREFIX, BUCKET).Value;

            return new PreviewAsset(id, mediaData, assetType, key, StatusMedia.UPLOADING, owner);
        }

    }
}
