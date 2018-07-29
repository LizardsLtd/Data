namespace Lizzards.Data.CQRS
{
    using System.Threading.Tasks;

    public interface IAsyncQuery<TPayload, TParameterNo1, TParameterNo2> : IsQuery
    {
        Task<TPayload> Execute(TParameterNo1 param1, TParameterNo2 param2);
    }
}