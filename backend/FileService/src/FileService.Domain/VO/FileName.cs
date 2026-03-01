using CSharpFunctionalExtensions;
using SharedKernel.Exseption;

namespace FileService.Domain.VO
{
    public record FileName
    {
        private FileName() { }
        private FileName(string name, string extention)
        {
            ValueName = name;
            Extention = extention;
        }

        public string ValueName { get; }

        public string Extention { get; }


        public static Result<FileName, Error> Create(string fileName)
        {
            int dotLast = fileName.LastIndexOf(".");
            if (!fileName.Contains(".") || dotLast == fileName.Length - 1 || dotLast == -1)
                return GeneralErrors.ValueNotValid("fiilName");

            var extention = fileName[(dotLast + 1)..].ToLower();

            return new FileName(fileName, extention);
        }
    }

}
