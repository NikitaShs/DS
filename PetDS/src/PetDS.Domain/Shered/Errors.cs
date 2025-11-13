using System.Collections;

namespace PetDS.Domain.Shered;

public class Errors : IEnumerable<Error>
{
    private readonly List<Error> _errors;

    public Errors(List<Error> errors) => _errors = [..errors];

    public IEnumerator<Error> GetEnumerator() => _errors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    public static implicit operator Errors(List<Error> errors) => new(errors);

    public static implicit operator Errors(Error errors) => new([errors]);
}