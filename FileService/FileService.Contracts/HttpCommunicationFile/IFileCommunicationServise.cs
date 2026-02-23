using CSharpFunctionalExtensions;
using FileService.Contracts.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using SharedKernel.Exseption;

namespace FileServise.Communication
{
    public interface IFileCommunicationServise
    {
        Task<Result<GetFileDto, Error>> GetMediaAsset(Guid id, CancellationToken cancellationToken);

        Task<Result<Guid, Error>> DeliteMediaAssets(Guid id, CancellationToken cancellationToken);

        Task<Result<IEnumerable<GetFileDto>, Error>> GetMediaAssets(IEnumerable<Guid> ids, CancellationToken cancellationToken);

    }
}
