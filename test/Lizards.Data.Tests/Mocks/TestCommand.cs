using System;
using Lizzards.Data.CQRS;

namespace Lizzards.Data.Tests.Mocks
{
    internal sealed class TestCommand : ICommand
    {
        public TestCommand()
        {
            CommandId = Guid.NewGuid();
        }

        public Guid CommandId { get; }
    }
}