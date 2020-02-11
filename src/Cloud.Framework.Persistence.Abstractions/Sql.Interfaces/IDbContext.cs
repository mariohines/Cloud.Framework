using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Cloud.Framework.Persistence.Abstractions.Sql.Interfaces
{
    /// <summary>
    /// Interface for a SQL database context.
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// Method to create a connection to the SQL database.
        /// </summary>
        /// <remarks>This method is usually used with a micro-ORM like Dapper when opening a connection to a SQL database.</remarks>
        /// <returns>Returns an implementation of <see cref="DbConnection"/>.</returns>
        DbConnection CreateDbConnection();
        
        /// <summary>
        /// Method to create an asynchronous SQL database transaction.
        /// </summary>
        /// <returns>Returns an task-based implementation of <see cref="DbTransaction"/>.</returns>
        Task<DbTransaction> CreateDbTransactionAsync();
    }
}