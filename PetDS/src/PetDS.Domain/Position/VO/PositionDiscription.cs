using CSharpFunctionalExtensions;
using PetDS.Domain.Shered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Domain.Position.VO
{
    public record PositionDiscription
    {

        private PositionDiscription(string valueDiscription)
        {
            ValueDiscription = valueDiscription;
        }

        public string ValueDiscription { get; }

        public static Result<PositionDiscription, Error> Create(string valueDiscription)
        {
            if(valueDiscription.Length >= Constans.MAX_1000_LENGHT_DESC)
            {
                return GeneralErrors.ValueNotValid("Discription");
            }

            return new PositionDiscription(valueDiscription);
        }

    }
}
