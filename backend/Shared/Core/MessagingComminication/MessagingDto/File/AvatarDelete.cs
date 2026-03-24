using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MessagingComminication.MessagingDto.File
{
    public record AvatarDelete(
        Guid AvatarId,
        string EntiteType,
        Guid AssetsType
        );
}
