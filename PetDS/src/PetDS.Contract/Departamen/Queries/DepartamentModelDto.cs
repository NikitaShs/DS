using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Contract.Departamen.Queries
{
    public record DepartamentModelDto
    {
        public string Name { get; private set; } = String.Empty;

        public string Identifier { get; private set; } = String.Empty;

        public Guid? ParentId { get; private set; }

        public string Path { get; private set; } = String.Empty;

        public short Depth { get; }

        public DateTime CreateAt { get; private set; }

        public DateTime UpdateAt { get; private set; }

        public Guid LocationId { get; private set; }
    }
}
