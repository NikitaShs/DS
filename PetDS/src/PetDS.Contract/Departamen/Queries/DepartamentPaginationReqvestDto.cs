using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Contract.Departamen.Queries
{
    public record DepartamentPaginationReqvestDto(int SizePage = 20, int Page = 1);
}
