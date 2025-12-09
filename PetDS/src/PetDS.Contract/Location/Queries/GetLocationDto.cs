using PetDS.Contract.Departamen.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Contract.Location.Queries
{
    public record GetLocationDto(List<LocationModelDto> locs, long totalCount);
}
