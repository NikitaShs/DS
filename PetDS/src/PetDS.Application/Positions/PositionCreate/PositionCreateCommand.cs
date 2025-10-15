using PetDS.Application.abcstractions;
using PetDS.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application.Positions.PositionCreate
{
    public record PositionCreateCommand(PositionCreateDto dto) : ICommand;
}
