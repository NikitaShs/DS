using CSharpFunctionalExtensions;
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

        public static Result<LocationName> Create(string valueName)
        {
            if (valueName.Length < 3 || valueName.Length > 120)
            {
                return Result.Failure<LocationName>("Невалидное имя");
            }

            return new LocationName(valueName);
        }
    }
}
