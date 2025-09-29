using CSharpFunctionalExtensions;
using PetDS.Domain.Shered;

namespace PetDS.Application.abcstractions
{
    public interface IHandler<ret, Tcommand> where Tcommand : ICommand
    {
        public Task<Result<ret, Error>> Handel(Tcommand command, CancellationToken cancellationToken = default);
    }

    public interface IHandler<Tcommand>
    {
        public Task<UnitResult<Error>> Handel(Tcommand command, CancellationToken cancellationToken = default);
    }
}