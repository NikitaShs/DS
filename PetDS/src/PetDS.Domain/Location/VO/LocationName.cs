using CSharpFunctionalExtensions;
using PetDS.Domain.Shered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Domain.Location.VO
{
    public record LocationName
    {
        private LocationName(string valueName)
        {
            ValueName = valueName;
        }

        public string ValueName { get; }

        public static Result<LocationName, Error> Create(string valueName)
        {
            if (valueName.Length < 3 || valueName.Length > Constans.MAX_120_lENGHT_LOC)
            {
                return GeneralErrors.ValueNotValid("Name");
            }

            return new LocationName(valueName);
        }
    }
}
