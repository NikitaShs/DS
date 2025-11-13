using PetDS.Domain.Shered;

namespace PetDS.Web.Response;

public record Envelope
{
    public Envelope(object? result, Errors? error)
    {
        Result = result;
        ErrorList = error;
        TimeGeneral = DateTime.UtcNow;
    }

    public object? Result { get; }

    public Errors? ErrorList { get; }
    public DateTime TimeGeneral { get; }

    public static Envelope Error(Errors error) => new(null, error);

    public static Envelope Ok(object? result = null) => new(result, null);
}