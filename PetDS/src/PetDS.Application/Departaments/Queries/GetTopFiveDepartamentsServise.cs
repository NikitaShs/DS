using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Contract.Departamen.Queries;
using PetDS.Domain.Shered;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application.Departaments.Queries
{
    public class GetTopFiveDepartamentsServise
    {
        private readonly IConnectionFactory _connectionFactory;

        private ILogger<GetTopFiveDepartamentsServise> _logger;

        public GetTopFiveDepartamentsServise(IConnectionFactory connectionFactory, ILogger<GetTopFiveDepartamentsServise> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task<Result<List<DepartamentModelDto>, Errors>> Handler(CancellationToken cancellationToken)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);

            var res = await connection.QueryAsync<DepartamentModelDto>("""
                                                  SELECT
                                                      dep.name,
                                                      dep.identifier,
                                                      dep.parent_id,
                                                      dep.depth,
                                                      dep.is_active,
                                                      dep.path,
                                                      COUNT(dep.id) as countPos
                                                  FROM departaments dep
                                                          JOIN "departamentPositions" dp ON dep.id = dp.departament_id
                                                  GROUP BY
                                                      dep.id
                                                   ORDER BY countPos DESC LIMIT 5;
                                                  """);

            return res.ToList();
        }
    }
}
