namespace Lizzards.Data.CQRS
{
    using System.Threading.Tasks;

    public interface IAsyncQuery<TPayload> : IsQuery
    {
        Task<TPayload> Execute();
    }
}