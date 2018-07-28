using System.Threading.Tasks;
using Lizzards.Data.Domain;

namespace Lizzards.Data.CQRS.DataAccess
{
    public interface IDataWriter<T>
        where T : IAggregateRoot
    {
        Task InsertNew(T item);

        Task UpdateExisting(T item);
    }
}