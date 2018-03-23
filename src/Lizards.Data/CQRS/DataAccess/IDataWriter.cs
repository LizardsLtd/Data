using System.Threading.Tasks;
using Lizards.Data.Domain;

namespace Lizards.Data.CQRS.DataAccess
{
    public interface IDataWriter<T>
        where T : IAggregateRoot
    {
        Task InsertNew(T item);

        Task UpdateExisting(T item);
    }
}