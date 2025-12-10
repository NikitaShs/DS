using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Contract.Location.Queries
{
    public record GetLocationFullRequestDto(
        string? Name,  string? City,
        string? Strit, int? NamberHouse,
        bool? IsActive, Guid[]? DepartamentIds,
        DateTime? TimeTo, DateTime? TimeAfter,
        int Page = 1, int SizePage = 20);
}
