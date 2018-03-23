using System;
using System.Threading.Tasks;

namespace Lizards.Data.CQRS
{
    public interface ICommandBus : IDisposable
    {
        Task Execute<TCommand>(TCommand command) where TCommand : ICommand;
    }
}