using System;
using Lizards.Data.CQRS;

namespace Lizards.Data.Tests.Mocks
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