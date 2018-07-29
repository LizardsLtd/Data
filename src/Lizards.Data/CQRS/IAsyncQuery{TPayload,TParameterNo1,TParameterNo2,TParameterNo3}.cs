namespace Lizzards.Data.CQRS
{
    using System.Threading.Tasks;

    public interface IAsyncQuery<TPayload, TParameterNo1, TParameterNo2, TParameterNo3> : IsQuery
    {
        Task<TPayload> Execute(TParameterNo1 param1, TParameterNo2 param2, TParameterNo3 param3);
    }
}