using CSharpFunctionalExtensions;
using SharedKernel.Exseption;

namespace PetDS.Domain.Location.VO;

public record LocationTimezone
{
    private LocationTimezone(string lanaCode) => LanaCode = lanaCode;

    public string LanaCode { get; }

    public static Result<LocationTimezone, Error> Create(string region, string city)
    {
        if (string.IsNullOrWhiteSpace(region) || string.IsNullOrWhiteSpace(city))
        {
            return GeneralErrors.ValueNotValid("lanaCode");
        }

        string lanaCode = $"{region}/{city}";

        return new LocationTimezone(lanaCode);
    }
}