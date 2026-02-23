using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Contracts.Dtos
{
    public record CompleteMultipartUploadRequest(Guid MediaAssetId, string UploadId, IReadOnlyList<PartETagDto> ETags);
}
