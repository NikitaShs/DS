using CSharpFunctionalExtensions;
using PetDS.Domain.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application
{
    public interface ILocationRepository
    {
        Task<Guid> AddLocation(Location location, CancellationToken cancellationToken);
    }
}
