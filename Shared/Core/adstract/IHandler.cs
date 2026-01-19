using CSharpFunctionalExtensions;
using SharedKernel.Exseption;

namespace Core.adstract;

public interface IHandler<ret, Tcommand> where Tcommand : ICommand
{
    Task<Result<ret, Errors>> Handler(Tcommand command, CancellationToken cancellationToken = default);
}

public interface IHandler<Tcommand> where Tcommand : ICommand
{
    Task<UnitResult<Errors>> Handler(Tcommand command, CancellationToken cancellationToken = default);
}