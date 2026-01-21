using Core.Adstract;
using PetDS.Contract.Departamen;

namespace PetDS.Application.Positions.PositionCreate;

public record PositionCreateCommand(PositionCreateDto dto) : ICommand;