using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Contract
{
    public record CreateDepartamentDto(string name, string identifier, List<Guid> locationsId, Guid? parantId = null);
}
