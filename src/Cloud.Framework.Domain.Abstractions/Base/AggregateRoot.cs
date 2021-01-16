#nullable enable

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
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
        [XmlIgnore, IgnoreDataMember]
        public ConcurrentQueue<IEvent> EventQueue { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="events">Collection of events to add to the root.</param>
        protected AggregateRoot(IEnumerable<IEvent>? events = null) {
            EventQueue = events != null
                             ? new ConcurrentQueue<IEvent>(events)
                             : new ConcurrentQueue<IEvent>();
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
        /// The identifier of this aggregate.
        /// </summary>
        public TId Id { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">The value of the <see cref="Id"/> property.</param>
        /// <param name="events">Collection of events to add to the root.</param>
        protected AggregateRoot(TId id, IEnumerable<IEvent>? events = null) : base(events) {
            Id = id;
        }
    }
}