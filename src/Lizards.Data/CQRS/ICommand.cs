using System;

namespace Lizards.Data.CQRS
{
    public interface ICommand
    {
        Guid CommandId { get; }
    }
}