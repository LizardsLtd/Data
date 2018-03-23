using System;
using System.Threading.Tasks;

namespace Lizards.Data.CQRS.DataAccess
{
    /// <summary>
    /// Allows to initialize data store at load.
    /// </summary>
    public interface IDataContextInitialiser : IDisposable
    {
        /// <summary>
        /// Asyncronus action to kick start data storage so the reader/writer will work every single time
        /// </summary>
        /// <returns>Asyncronus task</returns>
        Task Initialise();
    }
}