using CSharpFunctionalExtensions;
using PetDS.Domain.Shered;

namespace PetDS.Application.abcstractions
{
    public interface IHandler<ret, Tcommand> where Tcommand : ICommand
    {
        public Task<Result<ret, Errors>> Handel(Tcommand command, CancellationToken cancellationToken = default);
    }

    public interface IHandler<Tcommand> where Tcommand : ICommand
    {
        public Task<UnitResult<Errors>> Handel(Tcommand command, CancellationToken cancellationToken = default);
    }
}