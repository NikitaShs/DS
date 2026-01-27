using CSharpFunctionalExtensions;
using SharedKernel.Exseption;

namespace FileService.Domain.VO
{
    public record MediaData
    {
        private MediaData(FiilName fiilName, ContentType contentType, long size, int expectedChunksCount)
        {
            FiilName = fiilName;
            ContentType = contentType;
            Size = size;
            ExpectedChunksCount = expectedChunksCount;
        }

        public FiilName FiilName { get; }

        public ContentType ContentType { get; }

        public long Size { get; }

        public int ExpectedChunksCount { get; }

        public static Result<MediaData, Error> Create(FiilName fiilName, ContentType contentType, long size, int expectedChunksCount)
        {
            if (size <= 0)
                return GeneralErrors.ValueNotValid("size");

            if (expectedChunksCount <= 0)
                return GeneralErrors.ValueNotValid("expectedChunksCount");

            return new MediaData(fiilName, contentType, size, expectedChunksCount);
        }

    }

}
