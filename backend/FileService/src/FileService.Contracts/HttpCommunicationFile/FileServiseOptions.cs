using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Contracts.HttpCommunicationFile
{
    public record FileServiseOptions
    {
        public string Url { get; init; } = string.Empty;

        public int TimeoutSeconds { get; init; } = 10;
    }
}
