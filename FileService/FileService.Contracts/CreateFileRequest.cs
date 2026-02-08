using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Contracts
{
    public record CreateFileRequest(Guid EntiteId, string Context, string AssetType, int Size, string ContentType, string FileName);
}
