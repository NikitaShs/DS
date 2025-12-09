using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Contract.Departamen.Queries
{
    public record LocationModelDto
    {
        public string Name { get; init; }

        public string City { get; init; } = String.Empty;

        public string Strit { get; init; } = String.Empty;

        public string NamberHouse { get; init; } = String.Empty;

        public string LanaCode { get; init; } = String.Empty;

        public int TotalPage { get; init; }
    }
}
