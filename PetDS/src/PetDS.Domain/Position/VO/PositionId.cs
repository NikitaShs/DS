using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Domain.Position.VO
{
    public record PositionId
    {
        private PositionId(Guid vakueId)
        {
            ValueId = vakueId;
        }

        public Guid ValueId { get; }

        public static PositionId NewGuidPosition() => new (Guid.NewGuid());

        public static PositionId EmptyId() => new (Guid.Empty);

    }
}
