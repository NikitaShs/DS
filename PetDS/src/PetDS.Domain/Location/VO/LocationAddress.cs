using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Domain.Location.VO
{
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

        public static Result<LocationAddress> Create(string city, string strit, string namberHouse)
        {
            if(string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(strit) || string.IsNullOrWhiteSpace(namberHouse))
            {
                return Result.Failure<LocationAddress>("незаплненный адресс ");
            }

            return new LocationAddress(city, strit, namberHouse);

        }
    }
}
