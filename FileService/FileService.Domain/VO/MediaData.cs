using CSharpFunctionalExtensions;
using SharedKernel.Exseption;

namespace FileService.Domain.VO
{
    public record MediaData
    {
        private MediaData() { }
        private MediaData(FileName fileName, ContentType contentType, long size, int expectedChunksCount)
        {
            FileName = fileName;
            ContentType = contentType;
            Size = size;
            ExpectedChunksCount = expectedChunksCount;
        }

        public FileName FileName { get; init; }

        public ContentType ContentType { get; init; }

        public long Size { get; init; }

        public int ExpectedChunksCount { get; init; }

        public static Result<MediaData, Error> Create(FileName fileName, ContentType contentType, long size, int expectedChunksCount)
        {
            if (size <= 0)
                return GeneralErrors.ValueNotValid("size");

            if (expectedChunksCount <= 0)
                return GeneralErrors.ValueNotValid("expectedChunksCount");

            return new MediaData(fileName, contentType, size, expectedChunksCount);
        }

    }

}
