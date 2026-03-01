using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MessagingComminication.MessagingDto
{
    public record PreviewDelete(
        Guid PreviewId,
        string EntiteType,
        Guid AssetsType
        );
}
