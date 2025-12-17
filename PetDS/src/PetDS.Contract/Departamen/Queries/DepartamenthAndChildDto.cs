using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Contract.Departamen.Queries
{
    public record DepartamenthAndChildDto(List<DepartamenthAndChildModel> DepartamenthAndChildModels, long TotalCount);

}
