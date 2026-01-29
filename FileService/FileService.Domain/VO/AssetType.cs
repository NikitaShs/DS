namespace FileService.Domain.VO
{
    public enum AssetType
    {
        VIDEO,
        AVATAR,
        IMAGE,
        PREVIEW
    }

    public static class AssetTypeConvet
    {
        public static AssetType AssetTypeConvetToString(this string value)
        {
            return value switch
            {
                "video" => AssetType.VIDEO,
                "avatar" => AssetType.AVATAR,
                "image" => AssetType.IMAGE,
                "preview" => AssetType.PREVIEW,
                _ => throw new ArgumentException($"invalid asset type: {value}")
            };

        }
    }

}
