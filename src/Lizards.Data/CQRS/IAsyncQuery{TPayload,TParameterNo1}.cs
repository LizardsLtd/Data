namespace Lizzards.Data.CQRS
{
    using System.Threading.Tasks;

    public interface IAsyncQuery<TPayload, TParameterNo1> : IsQuery
    {
        Task<TPayload> Execute(TParameterNo1 param1);
    }
}