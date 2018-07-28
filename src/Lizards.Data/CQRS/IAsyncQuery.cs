namespace Lizzards.Data.CQRS
{
  using System.Threading.Tasks;

  public interface IAsyncQuery<TPayload> : IQuery<Task<TPayload>>
  {
  }
}
