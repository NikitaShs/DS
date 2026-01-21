using CSharpFunctionalExtensions;
using SharedKernel.Exseption;

namespace PetDS.Domain.Location.VO;

public record LocationAddress
{
    private LocationAddress()
    {
    }

    private LocationAddress(string city, string strit, string namberHouse)
    {
        City = city;
        Strit = strit;
        NamberHouse = namberHouse;
    }

    public string City { get; }

    public string Strit { get; }

    public string NamberHouse { get; }

    public static Result<LocationAddress, Error> Create(string city, string strit, string namberHouse)
    {
        if (string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(strit) ||
            string.IsNullOrWhiteSpace(namberHouse))
        {
            return GeneralErrors.ValueNotValid("Address");
        }

        return new LocationAddress(city, strit, namberHouse);
    }
}