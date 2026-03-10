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

        public FileName FileName { get; }

        public ContentType ContentType { get; }

        public long Size { get; }

        public int ExpectedChunksCount { get; }

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
