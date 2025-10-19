using PetDS.Application.abcstractions;
using PetDS.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application.Departaments.UpdateDepartament
{
    public record UpdateDepartamentLocationsCommand(UpdateDepartamentLocationsDto dto, Guid departamentId) : ICommand;
}
