using CSharpFunctionalExtensions;
using FileService.Core.abstractions;
using Microsoft.Extensions.Options;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.S3
{
    public class ChunkSizeCalculator : IChunkSizeCalculator
    {
        private readonly S3Options _options;

        public ChunkSizeCalculator(IOptions<S3Options> options)
        {
            _options = options.Value;
        }

        public Result<(long ChunckSize, int TotalChunks), Errors> Calculator(long fileSize)
        {
            if (_options.RecommendedChunkSizeBytes <= 0)
                return GeneralErrors.ValueFailure("recommendedChunkSizeBytes").ToErrors();
            if (_options.MaxChuncs <= 0)
                return GeneralErrors.ValueFailure("maxChuncs").ToErrors();

            if (fileSize <= _options.RecommendedChunkSizeBytes)
                return (fileSize, 1);

            var countChunks = (int)Math.Ceiling((double)fileSize / _options.RecommendedChunkSizeBytes);

            int resultCountChunks = Math.Min(countChunks, _options.MaxChuncs);

            long chunksSize = (fileSize + resultCountChunks - 1) / resultCountChunks;

            return (chunksSize, resultCountChunks);
        }
    }
}
