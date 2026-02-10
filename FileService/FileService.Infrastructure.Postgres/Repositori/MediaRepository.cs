using CSharpFunctionalExtensions;
using FileService.Contracts;
using FileService.Core.abstractions;
using FileService.Domain.Entites;
using FileService.Infrastructure.Postgres.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.Postgres.Repositori
{
    public class MediaRepository : IMediaRepository
    {
        private readonly PostgresDbContext _dbContext;
        private readonly ILogger<MediaRepository> _logger;

        public MediaRepository(PostgresDbContext dbContext, ILogger<MediaRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> AddFileAsync(MediaAsset mediaAsset, CancellationToken cancellationToken)
        {


            var res = _dbContext.MediaAsset.AddAsync(mediaAsset, cancellationToken);

            if (res.IsFaulted)
                return GeneralErrors.Unknown();

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                return mediaAsset.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "сохранение прошло не успешно");
                return Error.Failure("SaveChangesAsync.Failure", "сохранение прошло не успешно");
            }
        }

        public async Task<Result<int, Error>> DeleteFileAsync(Guid id, CancellationToken cancellationToken)
        {

            var deleteTime = DateTime.UtcNow.AddDays(10);
            var result = await _dbContext.Database.ExecuteSqlRawAsync("""UPDATE "MediaAsset" SET "IsActive" = false, "DeletedAt" = {0}, "UpdateAd" = CURRENT_TIMESTAMP WHERE "Id" = {1}""", [deleteTime, id], cancellationToken);

            return result;
        }

        public async Task<Result<MediaAsset, Error>> GetBy(Expression<Func<MediaAsset, bool>> expression, CancellationToken cancellationToken)
        {
            var res = await _dbContext.MediaAsset.FirstOrDefaultAsync(expression, cancellationToken);

            if (res == null)
            {
                _logger.LogInformation("неудалось получить файл");
                return GeneralErrors.ValueNotFound(null);
            }

            return res;
        }

        public async Task<Result<IEnumerable<MediaAsset>, Error>> GetByIdsAsync(IEnumerable<Guid> id, CancellationToken cancellationToken)
        {
            var res = await _dbContext.MediaAsset.Where(q => id.Contains(q.Id) && q.IsActive == true).ToListAsync(cancellationToken);
            if(res.Count == 0)
            {
                _logger.LogInformation("неудалось получить файлы");
                return GeneralErrors.ValueNotFound(null);
            }

            return res;
        }

        public async Task<Result<bool, Error>> SaveChangeAsync(CancellationToken cancellationToken)
        {
            try{
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError("ошибка при использовании SaveChangeAsync", ex);
                return Error.Conflict("SaveChangeAsync.IsFailure", "ошибка при использовании SaveChangeAsync");
            }
        }

    }
}
