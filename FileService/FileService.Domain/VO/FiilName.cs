using CSharpFunctionalExtensions;
using SharedKernel.Exseption;

namespace FileService.Domain.VO
{
    public record FiilName
    {
        private FiilName(string name, string extention)
        {
            ValueName = name;
            Extention = extention;
        }

        public string ValueName { get; }

        public string Extention { get; }


        public static Result<FiilName, Error> Create(string fiilName)
        {
            int dotLast = fiilName.LastIndexOf(".");
            if (!fiilName.Contains(".") || dotLast == fiilName.Length - 1 || dotLast == -1)
                return GeneralErrors.ValueNotValid("fiilName");

            var extention = fiilName[(dotLast + 1)..].ToLower();

            return new FiilName(fiilName, extention);
        }
    }

}
