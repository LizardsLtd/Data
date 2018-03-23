using System;
using Lizards.Data.Domain;

namespace Lizards.Data.CQRS.DataAccess
{
    /// <summary>
    /// Universal access to data store
    /// </summary>
    public interface IDataContext : IDisposable
    {
        /// <summary>
        /// Read access to data store
        /// </summary>
        /// <typeparam name="T">Type of the collection</typeparam>
        /// <param name="config">Additional configuration for data context</param>
        /// <returns>Read only object to query data store</returns>
        IDataReader<T> GetReader<T>()
            where T : IAggregateRoot;

        /// <summary>
        /// Write access to data store
        /// </summary>
        /// <typeparam name="T">Type of collection</typeparam>
        /// <param name="config">Additional configuration for data context</param>
        /// <returns>Write only object to update data store </returns>
        IDataWriter<T> GetWriter<T>()
             where T : IAggregateRoot;
    }
}