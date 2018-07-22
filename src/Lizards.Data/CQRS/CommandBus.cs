namespace Lizards.Data.CQRS
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public sealed class CommandBus : ICommandBus
    {
        private readonly IEnumerable<ICommandHandler> commandHandlers;

        private bool disposedValue = false;

        public CommandBus(IEnumerable<ICommandHandler> commandHandlers)
        {
            this.commandHandlers = commandHandlers;
        }

        public async Task Execute<TCommand>(TCommand command)
                where TCommand : ICommand
            => Parallel.ForEach(
                this.GetHandlersForCommand<TCommand>(command),
                handler => handler.Handle(command));

        public void Dispose() => Dispose(true);

        private IEnumerable<ICommandHandler<TCommand>> GetHandlersForCommand<TCommand>(ICommand command)
                where TCommand : ICommand
            => this.commandHandlers
                .Where(handler => this.CanHandle<TCommand>(handler))
                .Cast<ICommandHandler<TCommand>>();

        private bool CanHandle<TCommand>(ICommandHandler handler)
                where TCommand : ICommand
            => handler is ICommandHandler<TCommand>;

        private void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    foreach (var handler in this.commandHandlers)
                    {
                        handler.Dispose();
                    }
                }

                this.disposedValue = true;
            }
        }
    }
}