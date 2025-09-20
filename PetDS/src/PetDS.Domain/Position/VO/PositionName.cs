using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if(valueName.Length < 3 || valueName.Length > 100)
            {
                return Result.Failure<PositionName>("Невалидное имя");
            }

            return new PositionName(valueName);
        }
    }
}
