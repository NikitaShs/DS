using CSharpFunctionalExtensions;
using PetDS.Domain.Departament;
using PetDS.Domain.Position;
using PetDS.Domain.Shered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application.Positions
{
    public interface IPositionRepositiry
    {
        Task<Result<Guid, Errors>> AddPosition(Position position, CancellationToken cancellationToken);
    }
}
