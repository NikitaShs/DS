using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Contract.Departamen.Queries
{
    public record DepartamenthAndChildModel
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = String.Empty;

        public string Identifier { get; init; } = String.Empty;

        public Guid? ParentId { get; init; }

        public short Depth { get; init; }

        public string Path { get; init; } = String.Empty;

        public bool IsActive { get; init; }

        public bool HasMoreChildren { get; init; }

        public List<DepartamenthModelClear> Childs { get; init; }
    }
}
