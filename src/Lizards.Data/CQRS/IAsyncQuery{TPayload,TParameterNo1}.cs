namespace Lizards.Data.CQRS
{
  using System.Threading.Tasks;

  public interface IAsyncQuery<TPayload, TParameterNo1>
   : IQuery<Task<TPayload>, TParameterNo1>
  {
  }
}
