namespace Lizzards.Data.CQRS.DataAccess
{
  using System.Threading.Tasks;

  /// <summary>
  /// Contract for writing data.
  /// </summary>
  /// <typeparam name="T">Any possible IAggregateRoot type</typeparam>
  /// <typeparam name="TID">The type of the identifier.</typeparam>
  public interface IDataWriter<TPayload>
  {
    /// <summary>
    /// Inserts the new.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>Asynchronus void.</returns>
    Task InsertNew(TPayload item);

    /// <summary>
    /// Updates the existing.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>Asynchronus void.</returns>
    Task UpdateExisting(TPayload item);
  }
}
