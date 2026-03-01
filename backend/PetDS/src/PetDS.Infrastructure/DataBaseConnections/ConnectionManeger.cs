using Core.Adstract;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using SharedKernel.Exseption;

namespace PetDS.Infrastructure.DataBaseConnections;

public class ConnectionManeger : IConnectionManeger
{
    private readonly ApplicationDbContext _dbconection;
    private readonly ILogger<ConnectionManeger> _logger;
    private readonly ILoggerFactory _loggerFactory;

    public ConnectionManeger(ApplicationDbContext dbconection, ILogger<ConnectionManeger> logger,
        ILoggerFactory loggerFactory)
    {
        _dbconection = dbconection;
        _logger = logger;
        _loggerFactory = loggerFactory;
    }

    public async Task<Result<ITransactionScopes, Error>> CreateTranzit(CancellationToken cancellationToken)
    {
        try
        {
            ILogger<TransactionScopes> logger = _loggerFactory.CreateLogger<TransactionScopes>();
            IDbContextTransaction transaction = await _dbconection.Database.BeginTransactionAsync(cancellationToken);
            TransactionScopes transactionScope = new(transaction.GetDbTransaction(), logger);
            _logger.LogInformation("транзакция создана");
            return transactionScope;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "транзакция не создана ");
            return GeneralErrors.Unknown();
        }
    }

    public async Task<UnitResult<Error>> SaveChangeAsync()
    {
        try
        {
            _dbconection.SaveChanges();
            _logger.LogInformation("успешно сохранено");
            return UnitResult.Success<Error>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ошибка в созранением");
            return Error.Failure("fail.is.save", "fail save");
        }
    }
}