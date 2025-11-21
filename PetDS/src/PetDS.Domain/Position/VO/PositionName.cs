using CSharpFunctionalExtensions;
using PetDS.Domain.Shered;

namespace PetDS.Domain.Position.VO;

public record PositionName
{
    private PositionName(string valueName) => ValueName = valueName;

    public string ValueName { get; }

    public static Result<PositionName, Error> Create(string valueName)
    {
        if (valueName.Length < 3 || valueName.Length > Constans.MAX_100_lENGHT_POS)
        {
            return GeneralErrors.ValueNotValid("Name");
        }

        return new PositionName(valueName);
    }
}