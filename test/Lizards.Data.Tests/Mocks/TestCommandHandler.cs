using System.Threading.Tasks;
using Lizzards.Data.CQRS;
using Should.Fluent;

namespace Lizzards.Data.Tests.Mocks
{
    internal sealed class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public int handleCountCall;

        public TestCommandHandler()
        {
            this.handleCountCall = 0;
        }

        public void Dispose()
        {
        }

        public Task Handle(TestCommand command)
        {
            this.handleCountCall++;
            return Task.CompletedTask;
        }

        internal void HandleHasBeenRequestedOnce()
        {
            this.handleCountCall.Should().Equal(1);
        }
    }
}