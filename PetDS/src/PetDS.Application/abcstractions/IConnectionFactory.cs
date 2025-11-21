using System.Data.Common;

namespace PetDS.Application.abcstractions;

public interface IConnectionFactory
{
    Task<DbConnection> CreateConnectionAsync(CancellationToken cancellationToken);
}