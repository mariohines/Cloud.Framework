using System;

namespace Cloud.Framework.Core.Extensions
{
    /// <summary>
    /// Static class that holds extensions for strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Method that evaluates if a string is null or 'basically' empty.
        /// </summary>
        /// <param name="source">The string this method is executed on.</param>
        /// <returns>A boolean value.</returns>
        public static bool HasValue(this string? source) {
            return !string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// Method that evaluates if a string is null and returns empty if it is.
        /// </summary>
        /// <param name="source">The string this method is executed on.</param>
        /// <returns>The original string or an empty string.</returns>
        public static string EmptyIfNull(this string source) {
            return source.HasValue() ? source : string.Empty;
        }

        /// <summary>
        /// Method that evaluates if the contents of the string can be a boolean.
        /// </summary>
        /// <param name="source">The string this method is executed on.</param>
        /// <returns>A boolean value.</returns>
        public static bool ToBoolean(this string source) {
            return bool.TryParse(source, out var result) && result;
        }

        /// <summary>
        /// Method that appends a string value to another string.
        /// </summary>
        /// <param name="source">The string this method is executed on.</param>
        /// <param name="value">The value to append.</param>
        /// <returns>A new string value.</returns>
        public static string Append(this string source, string value) {
            return string.Concat(source.EmptyIfNull(), value);
        }

        /// <summary>
        /// Method that evaluates if a strings length is exclusively between 2 integer values.
        /// </summary>
        /// <param name="source">The string this method is executed on.</param>
        /// <param name="min">The minimum length of the string.</param>
        /// <param name="max">The maximum length of the string.</param>
        /// <returns>A boolean value.</returns>
        public static bool IsLengthBetween(this string source, int min, int max) {
            return source.HasValue() && source.Length >= min && source.Length <= max;
        }

        /// <summary>
        /// Method that formats a string with the passed in arguments.
        /// </summary>
        /// <remarks>The string MUST be in standard substitution format.</remarks>
        /// <example>
        /// var test = "{0} World!";
        /// var formattedTest = test.FormatWith("Hello");
        /// </example>
        /// <param name="source">The string this method is executed on.</param>
        /// <param name="arguments">A collection of arguments.</param>
        /// <returns>A formatted string.</returns>
        public static string FormatWith(this string source, params object[] arguments) {
            return string.Format(source, arguments);
        }

        /// <summary>
        /// Method that attempts to get a <see cref="DateTime"/> value from <paramref name="source"/>.
        /// </summary>
        /// <remarks>If the string can not be parsed into a <see cref="DateTime"/>, DateTime.UtcNow is returned.</remarks>
        /// <param name="source">The string this method is executed on.</param>
        /// <returns>A <see cref="DateTime"/> value.</returns>
        public static DateTime GetDateFromString(this string source) {
            return DateTime.TryParse(source, out var parsedDate) && parsedDate != default
                       ? parsedDate
                       : DateTime.UtcNow;
        }
    }
}