using PetDS.Domain.Departament;
using PetDS.Domain.Location;
using PetDS.Domain.Position;

namespace PetDS.Application.abcstractions;

public interface IReadDbContext
{
    IQueryable<Position> ReadPosition { get; }

    IQueryable<Location> ReadLocation { get; }

    IQueryable<Departament> ReadDepartament { get; }
}