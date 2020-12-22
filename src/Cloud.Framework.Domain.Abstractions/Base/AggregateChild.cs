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
        protected internal TParent? Parent { get; private set; }

        /// <summary>
        /// Method to set the <see cref="Parent"/> property.
        /// </summary>
        /// <param name="parent">The parent root.</param>
        public void SetParent(TParent parent) {
            Parent = parent;
        }

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
}