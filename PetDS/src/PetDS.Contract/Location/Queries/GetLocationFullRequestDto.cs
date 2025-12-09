using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Contract.Location.Queries
{
    public record GetLocationFullRequestDto(
        string? name,  string? city,
        string? strit, int? namberHouse,
        bool? isActive, Guid[]? DepartamentIds,
        DateTime? TimeTo, DateTime? TimeAfter,
        int page = 1, int sizePage = 20);
}
