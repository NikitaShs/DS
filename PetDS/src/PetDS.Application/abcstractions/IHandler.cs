using CSharpFunctionalExtensions;
using PetDS.Domain.Shered;

namespace PetDS.Application.abcstractions;

public interface IHandler<ret, Tcommand> where Tcommand : ICommand
{
    Task<Result<ret, Errors>> Handler(Tcommand command, CancellationToken cancellationToken = default);
}

public interface IHandler<Tcommand> where Tcommand : ICommand
{
    Task<UnitResult<Errors>> Handler(Tcommand command, CancellationToken cancellationToken = default);
}