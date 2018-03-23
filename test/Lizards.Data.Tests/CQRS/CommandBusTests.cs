using System.Threading.Tasks;
using Lizards.Data.CQRS;
using Lizards.Data.Tests.Mocks;
using Xunit;

namespace Lizards.Data.Tests.CQRS
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