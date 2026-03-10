using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Contracts.Dtos
{
    public record ChunkUploadUrl(
    int PartNumber,
    string UploadUrl);
}
