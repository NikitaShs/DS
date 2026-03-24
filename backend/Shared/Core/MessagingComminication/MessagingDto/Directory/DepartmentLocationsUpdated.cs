using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MessagingComminication.MessagingDto.Departament
{
    public record DepartmentLocationsUpdated(Guid depthId, string name, IEnumerable<Guid> locIds);
}
