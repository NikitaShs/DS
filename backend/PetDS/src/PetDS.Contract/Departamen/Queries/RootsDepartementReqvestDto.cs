namespace PetDS.Contract.Departamen.Queries;

public record RootsDepartementReqvestDto(int SizePage = 20, int Page = 1, int prefetch = 3);