using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cloud.Framework.Core.Abstract
{
    /// <summary>
    /// Abstract implementation of an enumeration class that boasts greater usability than a regular enumeration.
    /// </summary>
    /// <typeparam name="T">The value type for the enumeration.</typeparam>
    public abstract class Enumeration<T> : IComparable
        where T : struct, IComparable
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
        public override bool Equals(object obj) {
            if (!(obj is Enumeration<T> other)) {
                return false;
            }

            var doesTypeMatch = GetType() == obj.GetType();
            var doesValueMatch = Id.Equals(other.Id);

            return doesTypeMatch && doesValueMatch;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected bool Equals(Enumeration<T> other) {
            return Id.Equals(other.Id);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return Id.GetHashCode();
        }

        /// <inheritdoc />
        public int CompareTo(object obj) => Id.CompareTo(((Enumeration<T>) obj).Id);

        /// <summary>
        /// Get all the implementations of a specific <see cref="Enumeration{T}"/>.
        /// </summary>
        /// <typeparam name="TY">The type of <see cref="Enumeration{T}"/></typeparam>.
        /// <returns>A collection of <see cref="Enumeration{T}"/>.</returns>
        public static IEnumerable<TY> GetAll<TY>() where TY : Enumeration<T> {
            var fields = typeof(TY).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<TY>();
        }
    }
}