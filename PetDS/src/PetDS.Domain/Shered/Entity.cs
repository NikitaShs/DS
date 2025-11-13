namespace PetDS.Domain.Shered;

public abstract class Entity<TId>
{
    protected Entity(TId id) => Id = id;

    public TId Id { get; private set; }
}