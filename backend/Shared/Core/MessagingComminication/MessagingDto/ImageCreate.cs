using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MessagingComminication.MessagingDto
{
    public record ImageCreate(
        Guid ImageId,
        string EntiteType,
        Guid AssetsType
        );
}
