using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Contract.Departamen.Queries
{
    public record DepartamentModelDto
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = String.Empty;

        public string Identifier { get; init; } = String.Empty;

        public Guid? ParentId { get; init; }

        public short Depth { get; init; }

    }
}
