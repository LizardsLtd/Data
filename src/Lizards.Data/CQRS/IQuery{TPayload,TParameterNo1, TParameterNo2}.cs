namespace Lizards.Data.CQRS
{
    public interface IQuery<out TPayload, TParameterNo1, TParameterNo2> : IsQuery
    {
        TPayload Execute(TParameterNo1 param1, TParameterNo2 param2);
    }
}