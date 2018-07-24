namespace Lizards.Data.CQRS
{
    using System.Threading.Tasks;

    public interface IAsyncQuery<TPayload, TParameterNo1, TParameterNo2, TParameterNo3, TParameterNo4>
   : IQuery<Task<TPayload>, TParameterNo1, TParameterNo2, TParameterNo3, TParameterNo4>
    {
    }
}