using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Infrastructure.DataBaseConnections
{
    public interface IConnectionFactory
    {
        Task<DbConnection> CreateConnectionAsync(CancellationToken cancellationToken);
    }
}
