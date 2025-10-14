using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Contract
{
    public record PositionCreateDto(string name, List<Guid> departamentId, string discription = null);
}
