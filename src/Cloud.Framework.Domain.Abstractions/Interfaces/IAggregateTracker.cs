using System.Collections.Concurrent;
using Cloud.Framework.Domain.Abstractions.Base;

namespace Cloud.Framework.Domain.Abstractions.Interfaces
{
    /// <summary>
    /// An interface to implement an object that holds the responsibility of tracking changes to <see cref="AggregateRoot"/> objects through the scope of a request.
    /// </summary>
    public interface IAggregateTracker
    {
        /// <summary>
        /// The concurrent collection of <see cref="AggregateRoot"/> objects.
        /// </summary>
        ConcurrentQueue<AggregateRoot> AggregateQueue { get; }
        
        /// <summary>
        /// Method to add objects to <see cref="AggregateQueue"/>.
        /// </summary>
        /// <param name="root">The <see cref="AggregateRoot"/> to add.</param>
        void Add(AggregateRoot root);
    }
}