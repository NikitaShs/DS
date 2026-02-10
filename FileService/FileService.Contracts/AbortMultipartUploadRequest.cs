using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Contracts
{
    public record AbortMultipartUploadRequest(Guid MediaAssetId, string UploudId);
}
