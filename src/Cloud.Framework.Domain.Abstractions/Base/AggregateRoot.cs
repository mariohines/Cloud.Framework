using System.Collections.Concurrent;
using System.Collections.Generic;
using Cloud.Framework.Domain.Abstractions.Interfaces;

namespace Cloud.Framework.Domain.Abstractions.Base
{
    /// <summary>
    /// Abstract class for domain objects that are an AggregateRoot.
    /// </summary>
    public abstract class AggregateRoot
    {
        /// <summary>
        /// The concurrent collection of events associated to this object.
        /// </summary>
        public ConcurrentQueue<IEvent> EventQueue { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected AggregateRoot() {
            EventQueue = new ConcurrentQueue<IEvent>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="events">Collection of events to add to the root.</param>
        protected AggregateRoot(IEnumerable<IEvent> events) {
            EventQueue = new ConcurrentQueue<IEvent>(events);
        }

        /// <summary>
        /// Method to add events to this object.
        /// </summary>
        /// <param name="event">An object that implements <see cref="IEvent"/>.</param>
        public virtual void AddEvent(IEvent @event) {
            EventQueue.Enqueue(@event);
        }
    }

    /// <inheritdoc />
    /// <typeparam name="TId">The type for the <see cref="Id"/> of the root.</typeparam>
    public abstract class AggregateRoot<TId> : AggregateRoot
    {
        /// <summary>
        /// The identifier of this object.
        /// </summary>
        public TId Id { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">The value of the <see cref="Id"/> property.</param>
        protected AggregateRoot(TId id) {
            Id = id;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">The value of the <see cref="Id"/> property.</param>
        /// <param name="events">Collection of events to add to the root.</param>
        protected AggregateRoot(TId id, IEnumerable<IEvent> events) : base(events) {
            Id = id;
        }
    }
}