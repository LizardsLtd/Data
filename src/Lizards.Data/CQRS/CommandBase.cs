using System;

namespace Lizards.Data.CQRS
{
    public abstract class CommandBase : ICommand
    {
        public CommandBase()
        {
            this.CommandId = Guid.NewGuid();
        }

        public Guid CommandId { get; }
    }
}