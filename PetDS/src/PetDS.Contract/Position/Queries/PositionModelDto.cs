using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Contract.Departamen.Queries
{
    public record PositionModelDto
    {

        public string Name { get; init; } = string.Empty;

        public string Discription { get; init; } = string.Empty;

        public bool IsActive { get; init; }

    }
}
