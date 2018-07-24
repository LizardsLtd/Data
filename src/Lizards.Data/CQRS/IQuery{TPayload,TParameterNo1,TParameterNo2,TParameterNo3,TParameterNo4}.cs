namespace Lizards.Data.CQRS
{
    public interface IQuery<out TPayload, TParameterNo1, TParameterNo2, TParameterNo3, TParameterNo4> : IsQuery
    {
        TPayload Execute(TParameterNo1 param1, TParameterNo2 param2, TParameterNo3 param3, TParameterNo4 param4);
    }
}