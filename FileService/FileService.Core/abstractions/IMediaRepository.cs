using CSharpFunctionalExtensions;
using FileService.Domain.Entites;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.abstractions
{
    public interface IMediaRepository
    {
        Task<Result<Guid, Error>> CreateFileAsync(MediaAsset mediaAsset, CancellationToken cancellationToken);

        Task<Result<int, Error>> DeleteFileAsync(Guid id, CancellationToken cancellationToken);

        Task<Result<MediaAsset, Error>> GetFileByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<Result<IEnumerable<MediaAsset>, Error>> GetFilesByIdsAsync(IEnumerable<Guid> id, CancellationToken cancellationToken);

    }
}
