using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Contracts.Dtos
{
    public record UploadFileHandlerDto(string url, Guid MediaAssetId);
}
