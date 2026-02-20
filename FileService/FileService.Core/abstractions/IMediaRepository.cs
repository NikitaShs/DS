using CSharpFunctionalExtensions;
using FileService.Domain.Entites;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.abstractions
{
    public interface IMediaRepository
    {
        Task<Result<Guid, Error>> AddFileAsync(MediaAsset mediaAsset, CancellationToken cancellationToken);

        Task<Result<MediaAsset, Error>> GetBy(Expression<Func<MediaAsset, bool>> expression, CancellationToken cancellationToken);

        Task<Result<IEnumerable<MediaAsset>, Error>> GetByIdsAsync(IEnumerable<Guid> id, CancellationToken cancellationToken);

        Task<Result<bool, Error>> SaveChangeAsync(CancellationToken cancellationToken);

    }
}
