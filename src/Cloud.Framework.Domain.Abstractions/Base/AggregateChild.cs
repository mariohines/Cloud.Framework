#nullable enable

using System;
using Cloud.Framework.Domain.Abstractions.Interfaces;

namespace Cloud.Framework.Domain.Abstractions.Base
{
    /// <summary>
    /// An abstract class to implement an immediate child/grandchild of an <see cref="AggregateRoot"/>.
    /// </summary>
    /// <typeparam name="TParent">The type of the parent. Must inherit from <see cref="AggregateRoot"/>.</typeparam>
    public abstract class AggregateChild<TParent> : AggregateRoot
        where TParent : AggregateRoot
    {
        /// <summary>
        /// The parent root.
        /// </summary>
        protected internal TParent? Parent { get; protected set; }

        /// <summary>
        /// Method to set the <see cref="Parent"/> property.
        /// </summary>
        /// <param name="parent">The parent root.</param>
        public void SetParent(TParent parent) {
            Parent = parent;
        }

        /// <summary>
        /// Method to get the <see cref="Parent"/> property value.
        /// </summary>
        /// <returns>The parent root.</returns>
        public TParent? GetParent() => Parent;

        /// <summary>
        /// Overriden method to add an event. It's added to the parent events.
        /// </summary>
        /// <param name="event">The event to add.</param>
        /// <exception cref="InvalidOperationException">The exception thrown if the parent isn't set.</exception>
        public override void AddEvent(IEvent @event) {
            if(Parent == null) throw new InvalidOperationException($"'{nameof(Parent)}' property can not be null.");
            Parent.AddEvent(@event);
        }
    }

    /// <summary>
    /// An abstract class to implement an immediate child/grandchild of an <see cref="AggregateRoot"/>.
    /// </summary>
    /// <typeparam name="TId">The type for the <see cref="Id"/> of the child.</typeparam>
    /// <typeparam name="TParent">The type of the parent. Must inherit from <see cref="AggregateRoot"/>.</typeparam>
    public abstract class AggregateChild<TId, TParent> : AggregateChild<TParent>
        where TParent : AggregateRoot
    {
        /// <summary>
        /// The identifier of this aggregate child.
        /// </summary>
        public TId Id { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">The value of the <see cref="Id"/> property.</param>
        protected AggregateChild(TId id) {
            Id = id;
        }
    }
}