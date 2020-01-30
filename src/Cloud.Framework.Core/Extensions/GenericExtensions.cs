namespace Cloud.Framework.Core.Extensions
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Method that checks to see if the object calling this method is null.
        /// </summary>
        /// <param name="source">The object this method is executed on.</param>
        /// <typeparam name="TSource">Any object.</typeparam>
        /// <remarks>In the case that <paramref name="source"/> is a string, it will use the '.HasValue()' extension method on <paramref name="source"/>.</remarks>
        /// <returns>A boolean value.</returns>
        public static bool IsNull<TSource>(this TSource source) {
            return source is string stringType
                       ? !stringType.HasValue()
                       : source == null;
        }
    }
}