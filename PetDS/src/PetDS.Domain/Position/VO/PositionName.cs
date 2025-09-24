using CSharpFunctionalExtensions;
using PetDS.Domain.Shered;

namespace PetDS.Domain.Position.VO
{
    public record PositionName
    {
        private PositionName(string valueName)
        {
            ValueName = valueName;
        }

        public string ValueName { get; }

        public static Result<PositionName> Create (string valueName)
        {
            if(valueName.Length < 3 || valueName.Length > Constans.MAX_100_lENGHT_POS)
            {
                return Result.Failure<PositionName>("Невалидное имя");
            }

            return new PositionName(valueName);
        }
    }
}
