using System.Data.Common;

namespace Core.adstract;

public interface IConnectionFactory
{
    Task<DbConnection> CreateConnectionAsync(CancellationToken cancellationToken);
}