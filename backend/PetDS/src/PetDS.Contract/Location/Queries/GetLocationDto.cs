using PetDS.Contract.Departamen.Queries;

namespace PetDS.Contract.Location.Queries;

public record GetLocationDto(List<LocationModelDto> locs, long totalCount);