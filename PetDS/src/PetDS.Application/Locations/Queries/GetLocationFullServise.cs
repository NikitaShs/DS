using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Contract.Departamen.Queries;
using PetDS.Contract.Location.Queries;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Shered;
using System.Threading.Tasks;


namespace PetDS.Application.Locations.Queries
{
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
            var req = _readDbContext.ReadLocation;

            if (!string.IsNullOrEmpty(dto.name))
                req = req.Where(q => q.Name.ValueName.ToLower().Contains(dto.name.ToLower()));

            if (!string.IsNullOrEmpty(dto.strit))
                req = req.Where(q => q.Address.Strit.ToLower().Contains(dto.strit.ToLower()));

            if (!string.IsNullOrEmpty(dto.city))
                req = req.Where(q => q.Address.City.ToLower().Contains(dto.city.ToLower()));

            if (dto.namberHouse.HasValue)
                req = req.Where(q => q.Address.NamberHouse == dto.namberHouse.ToString());

            if (dto.TimeTo.HasValue)
                req = req.Where(q => q.CreateAt <= dto.TimeTo.Value.ToUniversalTime());

            if (dto.TimeAfter.HasValue)
                req = req.Where(q => q.CreateAt >= dto.TimeAfter.Value.ToUniversalTime());

            if (dto.isActive.HasValue)
                req = req.Where(q => q.IsActive == dto.isActive);

            if (dto.DepartamentIds != null && dto.DepartamentIds.Length > 0)
            {
                var departamentIds = dto.DepartamentIds.Select(id => DepartamentId.Create(id)).ToList();
                req = req.Where(q => _readDbContext.ReadDepartamentLocation.Any(dl => dl.LocationId == q.Id && departamentIds.Contains(dl.DepartamentId)));
            }

            var totalCount = await req.LongCountAsync();

            req = req
                .Skip((dto.page - 1) * dto.sizePage)
                .Take(dto.sizePage)
                .OrderBy(q => q.CreateAt);


            var res = await req.Select(q => new LocationModelDto
            {
                Name = q.Name.ValueName,
                City = q.Address.City,
                Strit = q.Address.Strit,
                NamberHouse = q.Address.NamberHouse,
                LanaCode = q.Timezone.LanaCode,
            }).ToListAsync(cancellationToken);

            return new GetLocationDto(res, totalCount);
        }
    }
}
