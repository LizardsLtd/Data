namespace Lizards.Data.CQRS
{
    public interface IQuery<out TPayload, TParameterNo1> : IsQuery
    {
        TPayload Execute(TParameterNo1);
    }
}