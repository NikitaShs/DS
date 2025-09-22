using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Domain.Location.VO
{
    public record LocationId
    {
        private LocationId(Guid valueId)
        {
            ValueId = valueId;
        }

        public Guid ValueId { get; }

        public static LocationId NewGuidLocation() => new(Guid.NewGuid());
        public static LocationId EmptyLocation() => new(Guid.Empty);
    }
}
