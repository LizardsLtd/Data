namespace Lizards.Data.CQRS
{
    public interface IsQuery
    {
    }

    public interface IQuery<out TPayload> : IsQuery
    {
        TPayload Execute();
    }
}