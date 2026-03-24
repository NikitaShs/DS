using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MessagingComminication.MessagingDto.Departament
{
    public record DepartamentCreate(Guid depthId, string name, string identifier);
}
