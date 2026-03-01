using System.Data.Common;

namespace Core.Adstract;

public interface IConnectionFactory
{
    Task<DbConnection> CreateConnectionAsync(CancellationToken cancellationToken);
}