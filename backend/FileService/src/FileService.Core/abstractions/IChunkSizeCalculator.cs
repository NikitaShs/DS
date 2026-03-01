using CSharpFunctionalExtensions;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.abstractions
{
    public interface IChunkSizeCalculator
    {
        Result<(long ChunckSize, int TotalChunks), Errors> Calculator(long fileSize);
    }
}
