using System.Threading.Tasks;
using Lizzards.Data.CQRS;
using Lizzards.Data.Tests.Mocks;
using Xunit;

namespace Lizzards.Data.Tests.CQRS
{
    public sealed class CommandBusTests
    {
        private readonly CommandBus commandBus;
        private readonly TestCommandHandler commandHandler;

        public CommandBusTests()
        {
            this.commandHandler = new TestCommandHandler();
            var handlers = new ICommandHandler[] { this.commandHandler };
            this.commandBus = new CommandBus(handlers);
        }

        [Fact]
        public async Task CommandBusCanCastHandlersToRequiredTypes()
        {
            var command = new TestCommand();

            await this.commandBus.Execute(command);

            this.commandHandler.HandleHasBeenRequestedOnce();
        }
    }
}