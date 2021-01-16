using System;
using System.Data;
using System.Threading;

namespace Cloud.Framework.Persistence.Abstractions.Sql.Base
{
    /// <summary>
    /// A struct to allow passing of information for calls to a SQL database.
    /// </summary>
    public readonly struct DbQuery
    {
        /// <summary>
        /// The sql query/command to execute on the database.
        /// </summary>
        public string Sql { get; }

        /// <summary>
        /// The parameters that are in the <see cref="Sql"/> property.
        /// </summary>
        public object? Parameters { get; }

        /// <summary>
        /// The type of the command being executed.
        /// </summary>
        public CommandType Type { get; }

        /// <summary>
        /// The transaction that this query should be part of.
        /// </summary>
        public IDbTransaction? Transaction { get; }

        /// <summary>
        /// The cancellation token for cancellable calls.
        /// </summary>
        public CancellationToken Token { get; }

        private DbQuery(string sql, object? parameters = null, CancellationToken token = default, CommandType type = CommandType.Text, IDbTransaction? transaction = null) {
            Sql = sql;
            Parameters = parameters;
            Token = token;
            Type = type;
            Transaction = transaction;
        }

        /// <summary>
        /// Method to create a DbQuery object.
        /// </summary>
        /// <param name="sql">The structured query language to execute.</param>
        /// <param name="parameters">The parameters used in the <paramref name="sql"/> statement.</param>
        /// <param name="token">The cancellation token for cancellable calls.</param>
        /// <param name="type">The <see cref="CommandType"/> for the <paramref name="sql"/> statement.</param>
        /// <param name="transaction">The transaction to use if available.</param>
        /// <returns>A DbQuery object.</returns>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="sql"/> argument is null or whitespace.</exception>
        public static DbQuery Create(string sql, object? parameters = null, CancellationToken token = default, CommandType type = CommandType.Text, IDbTransaction? transaction = null) {
            if (string.IsNullOrWhiteSpace(sql)) throw new ArgumentException($"The {nameof(sql)} parameter can not be null or whitespace.", nameof(sql));
            return new DbQuery(sql, parameters, token, type, transaction);
        }
    }
}