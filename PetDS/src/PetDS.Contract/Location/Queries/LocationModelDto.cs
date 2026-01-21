namespace PetDS.Contract.Departamen.Queries;

public record LocationModelDto
{
    public string Name { get; init; }

    public string City { get; init; } = string.Empty;

    public string Strit { get; init; } = string.Empty;

    public string NamberHouse { get; init; } = string.Empty;

    public string LanaCode { get; init; } = string.Empty;

    public int TotalPage { get; init; }
}