using System.Threading.Tasks;

namespace Lizards.Data.CQRS
{

  public interface IAsyncQuery<TPayload, TParameterNo1, TParameterNo2>
   : IQuery<Task<TPayload>, TParameterNo1, TParameterNo2>
  {
  }
}
