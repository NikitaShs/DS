using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Contracts
{
    public record CreateFileRequest(Guid entiteId, string context, string assetType, int size, string ContentType, string fileName);
}
