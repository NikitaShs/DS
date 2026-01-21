using Core.Adstract;
using PetDS.Contract;

namespace PetDS.Application.Locations.Commands.CreateLocation;

public record CreateLocationCommand(CreateLocationDto dto) : ICommand;