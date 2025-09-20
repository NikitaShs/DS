using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Domain.Departament.VO
{
    public record DepartamentLocationId
    {
        private DepartamentLocationId(Guid valueId)
        {
            ValueId = valueId;
        }

        public Guid ValueId { get; }

        public static DepartamentLocationId CreateNewGuid => new(Guid.NewGuid());

        public static DepartamentLocationId CreateEmpty => new(Guid.Empty);
    }
}
