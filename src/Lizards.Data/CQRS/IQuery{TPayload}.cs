namespace Lizzards.Data.CQRS
{
  public interface IQuery<out TPayload> : IsQuery
  {
    TPayload Execute();
  }
}
