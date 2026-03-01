using CSharpFunctionalExtensions;
using PetDS.Domain.Location.VO;
using SharedKernel.Exseption;

namespace PetDS.Domain.Location;

public class Location : SharedKernel.Exseption.Entity<LocationId>
{
    private Location(LocationId id) : base(id)
    {
    }

    private Location(LocationId id, LocationName locationName, LocationAddress address,
        LocationTimezone timezone) : base(id)
    {
        Name = locationName;
        Address = address;
        Timezone = timezone;
        IsActive = true;
        CreateAt = DateTime.UtcNow;
        UpdateAt = DateTime.UtcNow;
    }

    public LocationName Name { get; private set; }

    public LocationAddress Address { get; private set; }

    public LocationTimezone Timezone { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreateAt { get; private set; }

    public DateTime UpdateAt { get; private set; }

    public static Result<Location, Error> Create(LocationName locationName, string city, string region, string strit,
        string namberHouse)
    {
        LocationId id = LocationId.NewGuidLocation();

        Result<LocationAddress, Error> address = LocationAddress.Create(city, strit, namberHouse);

        if (address.IsFailure)
        {
            return GeneralErrors.ValueFailure("address");
        }

        Result<LocationTimezone, Error> timezone = LocationTimezone.Create(region, city);

        if (timezone.IsFailure)
        {
            return GeneralErrors.ValueFailure("timezone");
        }

        return new Location(id, locationName, address.Value, timezone.Value);
    }
}