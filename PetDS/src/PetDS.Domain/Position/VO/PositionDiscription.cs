using CSharpFunctionalExtensions;
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

        public static Result<PositionDiscription> Create(string valueDiscription)
        {
            if(valueDiscription.Length >= 1000)
            {
                return Result.Failure<PositionDiscription>("это слишком");
            }

            return new PositionDiscription(valueDiscription);
        }

    }
}
