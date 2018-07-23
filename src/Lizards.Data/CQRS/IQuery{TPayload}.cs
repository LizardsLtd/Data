namespace Lizards.Data.CQRS
{
  public interface IQuery<out TPayload> : IsQuery
  {
    TPayload Execute();
  }
}
