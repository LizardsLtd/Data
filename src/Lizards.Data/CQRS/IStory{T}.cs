namespace Lizzards.Data.CQRS
{
    using System.Threading.Tasks;

    public interface IStory<T> : IStory
    {
        Task Execute(T payload);
    }
}