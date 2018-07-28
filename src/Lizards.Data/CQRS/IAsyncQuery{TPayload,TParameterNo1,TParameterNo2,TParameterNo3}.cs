namespace Lizzards.Data.CQRS
{
    using System.Threading.Tasks;

    public interface IAsyncQuery<TPayload, TParameterNo1, TParameterNo2, TParameterNo3>
      : IQuery<Task<TPayload>, TParameterNo1, TParameterNo2, TParameterNo3>
    {
    }
}