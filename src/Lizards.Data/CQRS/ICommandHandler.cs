using System;
using System.Threading.Tasks;

namespace Lizards.Data.CQRS
{
    public interface ICommandHandler : IDisposable { }

    public interface ICommandHandler<TCommand> : ICommandHandler
        where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}