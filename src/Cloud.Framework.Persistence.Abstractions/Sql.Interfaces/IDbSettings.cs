using System.Data;

namespace Cloud.Framework.Persistence.Abstractions.Sql.Interfaces
{
    /// <summary>
    /// Interface for SQL database settings.
    /// </summary>
    /// <remarks>Implementors of this interface should normally take a SQL connectionString as a parameter.</remarks>
    /// <typeparam name="TConnection">Any implementor of the <see cref="IDbConnection"/> interface.</typeparam>
    public interface IDbSettings<out TConnection> where TConnection : IDbConnection
    {
        /// <summary>
        /// Method to create a SQL database connection.
        /// </summary>
        /// <remarks>This method is usually used with a micro-ORM like Dapper when opening a connection to a SQL database.</remarks>
        /// <returns>An implementor of <see cref="IDbConnection"/>.</returns>
        TConnection CreateDbConnection();
    }
}