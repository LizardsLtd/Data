using System.Threading.Tasks;

namespace Lizards.Data.CQRS
{

  public interface IAsyncQuery<TPayload, TParameterNo1>
   : IQuery<Task<TPayload>, TParameterNo1>
  {
  }
}
