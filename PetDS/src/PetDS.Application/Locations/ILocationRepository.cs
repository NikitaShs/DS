using CSharpFunctionalExtensions;
using PetDS.Domain.Location;
using PetDS.Domain.Shered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application.Locations
{
    public interface ILocationRepository
    {
        Task<Result<Guid, Error>> AddLocation(Location location, CancellationToken cancellationToken = default);
    }
}
