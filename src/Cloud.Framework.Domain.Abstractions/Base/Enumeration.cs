#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cloud.Framework.Domain.Abstractions.Base
{
    /// <summary>
    /// Abstract implementation of an enumeration class that boasts greater usability than a regular enumeration.
    /// </summary>
    /// <typeparam name="T">The value type for the enumeration.</typeparam>
    public abstract class Enumeration<T> : IComparable<Enumeration<T>>, IEquatable<Enumeration<T>>
        where T : struct, IComparable<T>
    {
        /// <summary>The display name for the enumeration.</summary>
        public string Name { get; }

        /// <summary>The value of the enumeration.</summary>
        public T Id { get; }

        /// <summary>The constructor for creating an enumeration class.</summary>
        /// <param name="id">The value of the enumeration.</param>
        /// <param name="name">The display name of the enumeration.</param>
        protected Enumeration(T id, string name) {
            Id = id;
            Name = name;
        }

        /// <inheritdoc />
        public override string ToString() => Name;

        /// <inheritdoc />
        public int CompareTo(Enumeration<T>? other) {
            if (other == null) return -1;
            return Id.CompareTo(other.Id);
        }

        /// <inheritdoc />
        public bool Equals(Enumeration<T>? other) => Id.Equals(other?.Id);

        /// <inheritdoc />
        public override int GetHashCode() {
            return Id.GetHashCode() + Name.GetHashCode();
        }


        /// <summary>
        /// Get all the implementations of a specific <see cref="Enumeration{T}"/>.
        /// </summary>
        /// <typeparam name="TEnumeration">The type of <see cref="Enumeration{T}"/></typeparam>.
        /// <returns>A collection of <see cref="Enumeration{T}"/>.</returns>
        public static IEnumerable<TEnumeration> GetAll<TEnumeration>() where TEnumeration : Enumeration<T> {
            var fields = typeof(TEnumeration).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<TEnumeration>();
        }
    }
}