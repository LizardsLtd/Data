using System;

namespace Lizzards.Data.CQRS
{
    public interface ICommand
    {
        Guid CommandId { get; }
    }
}