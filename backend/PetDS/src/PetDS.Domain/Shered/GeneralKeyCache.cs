namespace PetDS.Domain.Shered;

public class GeneralKeyCache
{
    public static string KeyDeptById(Guid id) => $"departament_id{id}";

    public static string KeyDeptByIdentifier(string identifier) => $"departament_identifier{identifier}";

    public static string KeyDeptTops(string typeTop, int count) => $"departamentTopBy_{typeTop}_count{count}";

    public static string KeyDeptByIn(string identifier) => $"departament_identifier{identifier}";

    public static string KeyDeptRootsPagination(
        int page = 1,
        int sizePage = 20,
        int prefetch = 3) => $"departamentRoots_page{page}_size{sizePage}_prefetch{prefetch}";

    public static string KeyDeptChildPagination(
        Guid parentId,
        int page = 1,
        int sizePage = 20) => $"departamentChild_parentId{parentId}_page{page}_size{sizePage}";

    public static string KeyDeptByFilterPagination(
        string? name = null,
        string? depth = null,
        Guid? parentId = null,
        bool? parent = null,
        bool isActive = true,
        int prefetch = 3,
        int page = 1,
        int sizePage = 20
    ) =>
        $"departament_name{name}_depth{depth}_parentId{parentId}_parent{parent}_prefetch{prefetch}_page{page}_size{sizePage}";
}