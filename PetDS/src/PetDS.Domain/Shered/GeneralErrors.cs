namespace PetDS.Domain.Shered;

public class GeneralErrors
{
    public static Error ValueNotValid(string? nameValue)
    {
        string value = nameValue ?? "value";
        return Error.Validation("value.is.Invalid", $"{value} is invalid");
    }

    public static Error ValueNotFound(Guid? IdValue)
    {
        string id = IdValue == null ? " " : $"for id '{IdValue}'";
        return Error.Validation("value.not.Found", $"record not Found {id}");
    }

    public static Error ValueFailure(string? nameValue)
    {
        string value = nameValue ?? "value";
        return Error.Validation("value.not.create", $"{value} not create");
    }

    public static Error Unknown() => Error.Unknown("error.is.unknown", "error of unknown type");

    public static Error Update(string nameValue) => Error.Update("update.is.failed", $"update {nameValue} failed");
}