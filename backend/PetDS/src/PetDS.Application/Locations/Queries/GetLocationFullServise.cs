using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Contract.Departamen.Queries;
using PetDS.Contract.Location.Queries;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location;

namespace PetDS.Application.Locations.Queries;

public class GetLocationFullServise
{
    private readonly ILogger<GetLocationFullServise> _logger;
    private readonly IReadDbContext _readDbContext;

    public GetLocationFullServise(ILogger<GetLocationFullServise> logger, IReadDbContext readDbContext)
    {
        _logger = logger;
        _readDbContext = readDbContext;
    }

    public async Task<GetLocationDto> Handler(GetLocationFullRequestDto dto, CancellationToken cancellationToken)
    {
        IQueryable<Location> req = _readDbContext.ReadLocation;

        if (!string.IsNullOrEmpty(dto.Name))
        {
            req = req.Where(q => q.Name.ValueName.ToLower().Contains(dto.Name.ToLower()));
        }

        if (!string.IsNullOrEmpty(dto.Strit))
        {
            req = req.Where(q => q.Address.Strit.ToLower().Contains(dto.Strit.ToLower()));
        }

        if (!string.IsNullOrEmpty(dto.City))
        {
            req = req.Where(q => q.Address.City.ToLower().Contains(dto.City.ToLower()));
        }

        if (dto.NamberHouse.HasValue)
        {
            req = req.Where(q => q.Address.NamberHouse == dto.NamberHouse.ToString());
        }

        if (dto.TimeTo.HasValue)
        {
            req = req.Where(q => q.CreateAt <= dto.TimeTo.Value.ToUniversalTime());
        }

        if (dto.TimeAfter.HasValue)
        {
            req = req.Where(q => q.CreateAt >= dto.TimeAfter.Value.ToUniversalTime());
        }

        if (dto.IsActive.HasValue)
        {
            req = req.Where(q => q.IsActive == dto.IsActive);
        }

        if (dto.DepartamentIds != null && dto.DepartamentIds.Length > 0)
        {
            List<DepartamentId> departamentIds = dto.DepartamentIds.Select(id => DepartamentId.Create(id)).ToList();
            req = req.Where(q =>
                _readDbContext.ReadDepartamentLocation.Any(dl =>
                    dl.LocationId == q.Id && departamentIds.Contains(dl.DepartamentId)));
        }

        long totalCount = await req.LongCountAsync();

        req = req
            .Skip((dto.Page - 1) * dto.SizePage)
            .Take(dto.SizePage)
            .OrderBy(q => q.CreateAt);


        List<LocationModelDto> res = await req.Select(q => new LocationModelDto
        {
            Name = q.Name.ValueName,
            City = q.Address.City,
            Strit = q.Address.Strit,
            NamberHouse = q.Address.NamberHouse,
            LanaCode = q.Timezone.LanaCode
        }).ToListAsync(cancellationToken);

        return new GetLocationDto(res, totalCount);
    }
}