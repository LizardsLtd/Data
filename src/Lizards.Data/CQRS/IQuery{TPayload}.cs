namespace Lizzards.Data.CQRS
{
  using System.Threading.Tasks;

  public interface IQuery<TPayload> : IsQuery
  {
    Task<TPayload> Execute();
  }
}
