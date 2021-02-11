using System;
using System.Linq;

namespace Cloud.Framework.Domain.Abstractions.Types
{
    /// <summary>
    /// A standard type to handle data that is a phone number.
    /// </summary>
    /// <remarks>This has room for improvement. It only handles North American phone numbers at the moment.</remarks>
    public readonly struct PhoneNumber
    {
        /// <summary>
        /// The area code of the phone number.
        /// </summary>
        public string AreaCode { get; }
        
        /// <summary>
        /// The actual phone number.
        /// </summary>
        public string Number { get; }
        
        /// <summary>
        /// The country code of the phone number.
        /// </summary>
        /// <remarks>Currently not in use.</remarks>
        public string CountryCode { get; }

        internal PhoneNumber(string areaCode, string number, string countryCode = "1") {
            AreaCode = areaCode;
            Number = number;
            CountryCode = countryCode;
        }

        /// <inheritdoc />
        public override string ToString() {
            return $"{AreaCode}{Number}";
        }

        /// <summary>
        /// The method to get a formatted display of the phone number.
        /// </summary>
        /// <param name="format">The format that the phone number should be put in.</param>
        /// <returns>A formatted string representation of the phone number.</returns>
        public string ToDisplay(string format = "({0}) {1}-{2}") {
            var firstThreeDigits = Number.Substring(0, 3);
            var lastFourDigits = Number.Substring(3);
            return string.Format(format, AreaCode, firstThreeDigits, lastFourDigits);
        }

        /// <summary>
        /// The method to create a <see cref="PhoneNumber"/> type.
        /// </summary>
        /// <param name="source">The string used to create the phone number type.</param>
        /// <returns>A <see cref="PhoneNumber"/> type.</returns>
        /// <exception cref="ArgumentException">The error returned when the string isn't a valid phone number.</exception>
        public static PhoneNumber Create(string source) {
            var parsed = string.Join(string.Empty, source.Where(char.IsDigit));
            if (parsed.Length < 10 || parsed.Length > 11) throw new ArgumentException($"'{source}' is not a valid phone number.", nameof(source));
            var countryCode = "1";
            if (parsed.Length == 11) {
                countryCode = parsed.Substring(0, 1);
                parsed = parsed.Substring(1);
            }

            return new PhoneNumber(parsed.Substring(0, 3), parsed.Substring(3), countryCode);
        }

        /// <summary>
        /// Implicit operator to take a phone number string and convert it ot the type.
        /// </summary>
        /// <param name="source">The string representation of a phone number.</param>
        /// <returns>A <see cref="PhoneNumber"/> type.</returns>
        public static implicit operator PhoneNumber(string source) {
            return Create(source);
        }
    }
}