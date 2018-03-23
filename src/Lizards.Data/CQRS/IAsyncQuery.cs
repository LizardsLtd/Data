using System.Threading.Tasks;

namespace Lizards.Data.CQRS
{
    public interface IAsyncQuery<TPayload> : IQuery<Task<TPayload>>
    {
    }
}