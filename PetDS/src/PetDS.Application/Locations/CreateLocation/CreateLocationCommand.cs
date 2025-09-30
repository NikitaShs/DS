using PetDS.Application.abcstractions;
using PetDS.Contract;

namespace PetDS.Application.Locations.CreateLocation
{
    public record CreateLocationCommand(CreateLocationDto dto) : ICommand;
}