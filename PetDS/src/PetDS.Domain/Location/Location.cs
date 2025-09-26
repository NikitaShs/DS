using CSharpFunctionalExtensions;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location.VO;
using PetDS.Domain.Shered;

namespace PetDS.Domain.Location
{
    public class Location : Shered.Entity<LocationId>
    {
        private Location(LocationId id) : base(id)
        {
        }

        private Location(LocationId id, LocationName locationName, LocationAddress address, LocationTimezone timezone) : base(id)
        {
            Name = locationName;
            Address = address;
            Timezone = timezone;
            IsActive = true;
            CreateAt = DateTime.UtcNow;
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public LocationName Name { get; private set; }

        public LocationAddress Address { get; private set; }

        public LocationTimezone Timezone { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreateAt { get; private set; }

        public DateTime UpdateAt { get; private set; }

        public static Result<Location> Create(LocationName locationName, string city, string region, string strit, string namberHouse)
        {
            var id = LocationId.NewGuidLocation();

            var address = LocationAddress.Create(city, strit, namberHouse);

            var timezone = LocationTimezone.Create(region, city);

            return new Location(id, locationName, address.Value, timezone.Value);
        }
    }

}