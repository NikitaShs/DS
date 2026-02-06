using CSharpFunctionalExtensions;
using SharedKernel.Exseption;

namespace FileService.Domain.VO
{
    public record ContentType
    {
        private ContentType() { }
        private ContentType(string valueContentType, MediaType mediaType)
        {
            ValueContentType = valueContentType;
            MediaType = mediaType;
        }

        public string ValueContentType { get; init; }

        public MediaType MediaType { get; init; }

        public static Result<ContentType, Error> Create(string contentType)
        {
            if(string.IsNullOrWhiteSpace(contentType))
                return GeneralErrors.ValueNotValid("contentType");

            MediaType mediaType = contentType switch
            {
                var ct when ct.Contains("video") => MediaType.VIDEO,
                var ct when ct.Contains("audio") => MediaType.AUDIO,
                var ct when ct.Contains("document") => MediaType.DOCUMENT,
                var ct when ct.Contains("images") => MediaType.IMAGE,
                var ct => MediaType.UNKNOWN
            };

            return new ContentType(contentType, mediaType);
        }
    }

}
