using CSharpFunctionalExtensions;
using FileService.Contracts.Dtos;
using Microsoft.Extensions.Logging;
using SharedKernel.Exseption;

namespace FileServise.Communication
{
    internal sealed class FileHttpClient : IFileCommunicationServise
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger<FileHttpClient> _logger;

        public FileHttpClient(HttpClient httpClient, ILogger<FileHttpClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Result<GetFileDto, Error>> GetMediaAsset(Guid MediaId, CancellationToken cancellationToken)
        {
            try
            {

                HttpResponseMessage response = await _httpClient.GetAsync($"files/{MediaId}");

                return await response.HandlerResponseAsync<GetFileDto>(cancellationToken);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "неудалось получить media asset с id {id}", MediaId);

                return Error.Failure("server.internal", "Failed to request media asset info");

            }
        }

        public async Task<Result<Guid, Error>> DeliteMediaAssets(Guid MediaId, CancellationToken cancellationToken)
        {
            try
            {

                HttpResponseMessage response = await _httpClient.DeleteAsync($"files/del/{MediaId}");

                return await response.HandlerResponseAsync<Guid>(cancellationToken);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "неудалось удалить media asset с id {id}", MediaId);

                return Error.Failure("server.internal", "Failed to request media asset info");

            }
        }

        public async Task<Result<IEnumerable<GetFileDto>, Error>> GetMediaAssets(IEnumerable<Guid> MediaIds, CancellationToken cancellationToken)
        {
            try
            {

                HttpResponseMessage response = await _httpClient.GetAsync($"files/ids?{MediaIds}");

                return await response.HandlerResponseAsync<IEnumerable<GetFileDto>>(cancellationToken);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "неудалось получить media asset с одним из предложнных id");

                return Error.Failure("server.internal", "Failed to request media asset info");

            }
        }
    }
}
