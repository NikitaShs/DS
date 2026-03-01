using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MessagingComminication.MessagingDto
{
    public record ImageDelete(
        Guid ImageId,
        string EntiteType,
        Guid AssetsType
        );
}
