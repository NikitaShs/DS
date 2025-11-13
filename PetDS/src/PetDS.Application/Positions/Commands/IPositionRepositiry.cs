using CSharpFunctionalExtensions;
using PetDS.Domain.Position;
using PetDS.Domain.Shered;

namespace PetDS.Application.Positions;

public interface IPositionRepositiry
{
    Task<Result<Guid, Errors>> AddPosition(Position position, CancellationToken cancellationToken);
}