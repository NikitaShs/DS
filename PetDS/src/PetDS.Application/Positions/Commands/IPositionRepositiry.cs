using CSharpFunctionalExtensions;
using PetDS.Domain.Position;
using PetDS.Domain.Shered;
using SharedKernel.Exseption;

namespace PetDS.Application.Positions;

public interface IPositionRepositiry
{
    Task<Result<Guid, Errors>> AddPosition(Position position, CancellationToken cancellationToken);
}