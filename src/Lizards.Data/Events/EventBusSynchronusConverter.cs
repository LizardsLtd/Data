using System.Threading.Tasks;

namespace Lizzards.Data.Events
{
    public sealed class EventBusSynchronusConverter<T>
        where T : IEvent
    {
        private T result;

        public static EventBusSynchronusConverter<T> Setup(IEventBus bus)
        {
            var synchronizer = new EventBusSynchronusConverter<T>();

            bus.Subscribe<T>(@event => synchronizer.SetResult(@event));

            return synchronizer;
        }

        public async Task<T> GetResult(int tinmeoutInSeconds = 30)
        {
            var retryAttepmts = tinmeoutInSeconds * 4;

            while (retryAttepmts > 0)
            {
                await Task.Delay(2000);
                retryAttepmts--;

                if (this.result != null)
                {
                    break;
                }
            }

            return this.result;
        }

        private void SetResult(T eventResult) => this.result = eventResult;
    }
}