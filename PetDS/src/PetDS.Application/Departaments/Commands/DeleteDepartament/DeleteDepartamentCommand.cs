using PetDS.Application.abcstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application.Departaments.Commands.DeleteDepartament
{
    public record DeleteDepartamentCommand(Guid departamenId) : ICommand;
}
