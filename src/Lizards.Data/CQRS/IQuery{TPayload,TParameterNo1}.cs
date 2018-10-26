namespace Lizzards.Data.CQRS
{
  using System.Threading.Tasks;

  public interface IQuery<TPayload, TOptions> : IsQuery
  {
    Task<TPayload> Execute(TOptions param1);
  }
}
