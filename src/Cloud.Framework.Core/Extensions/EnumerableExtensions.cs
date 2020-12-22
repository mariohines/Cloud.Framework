using System;
using System.Collections.Generic;
using System.Linq;

namespace Cloud.Framework.Core.Extensions
{
    /// <summary>
    /// Static class to hold enumerable extensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Method that converts a collection into a batch for handling chunks.
        /// </summary>
        /// <param name="source">The original source collection.</param>
        /// <param name="batchSize">The amount of records to break into smaller collections.</param>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <returns>A collection of a collection to be iterated across.</returns>
        /// <remarks>This method is usually used in large collections of objects.</remarks>
        public static IEnumerable<IEnumerable<T>> AsBatch<T>(this IEnumerable<T> source, int batchSize) {
            var collectionSet = source.ToList();
            for (var start = 0; start < collectionSet.Count; start += batchSize) {
                yield return collectionSet.Skip(start).Take(batchSize);
            }
        }

        /// <summary>
        /// Method to executes an action on each item of the collection.
        /// </summary>
        /// <param name="source">The source collection.</param>
        /// <param name="action">The action to be executed on each item of <paramref name="source"/>.</param>
        /// <typeparam name="T">The type of object.</typeparam>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
            foreach (var item in source) {
                action(item);
            }
        }
    }
}