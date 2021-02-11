using System;

namespace Cloud.Framework.Domain.Abstractions.Types
{
    /// <summary>
    /// A standard type to handle data that is an address.
    /// </summary>
    /// <remarks>This has room for improvement. It only handles North American addresses at the moment.</remarks>
    public readonly struct Address : IEquatable<Address>
    {
        /// <summary>
        /// The street and number of the address.
        /// </summary>
        public string Street { get; }
        
        /// <summary>
        /// The city of the address.
        /// </summary>
        public string City { get; }
        
        /// <summary>
        /// The state of the address.
        /// </summary>
        public string State { get; }
        
        /// <summary>
        /// The postal code of the address.
        /// </summary>
        public string PostalCode { get; }
        
        /// <summary>
        /// The apartment or suite of the address. (Optional)
        /// </summary>
        /// <remarks>This isn't required.</remarks>
        public string? ApartmentOrSuite { get; }
        
        internal Address(string street, string city, string state, string postalCode, string? apartmentOrSuite = default) {
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            ApartmentOrSuite = apartmentOrSuite;
        }

        /// <inheritdoc />
        public override string ToString() {
            return string.IsNullOrWhiteSpace(ApartmentOrSuite)
                       ? $"{Street}, {City}, {State} {PostalCode}"
                       : $"{Street} {ApartmentOrSuite}, {City}, {State} {PostalCode}";
        }

        /// <summary>
        /// The method used to create an <see cref="Address"/> object.
        /// </summary>
        /// <param name="street"><see cref="Street"/></param>
        /// <param name="city"><see cref="City"/></param>
        /// <param name="state"><see cref="State"/></param>
        /// <param name="postalCode"><see cref="PostalCode"/></param>
        /// <param name="apartmentOrSuite"><see cref="ApartmentOrSuite"/></param>
        /// <exception cref="ArgumentNullException">All non-null parameters are required.</exception>
        public static Address Create(string street, string city, string state, string postalCode, string? apartmentOrSuite = default) {
            if (string.IsNullOrWhiteSpace(street)) throw new ArgumentNullException(nameof(street));
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentNullException(nameof(city));
            if (string.IsNullOrWhiteSpace(state)) throw new ArgumentNullException(nameof(state));
            if (string.IsNullOrWhiteSpace(postalCode)) throw new ArgumentNullException(nameof(postalCode));
            
            return new Address(street, city, state, postalCode, apartmentOrSuite);
        }
        
        /// <summary>
        /// Implicit operator to convert an <see cref="Address"/> type to a string.
        /// </summary>
        /// <param name="address">The <see cref="Address"/> type to convert to string.</param>
        /// <returns>A string representation of an <see cref="Address"/>.</returns>
        public static implicit operator string(Address address) {
            return address.ToString();
        }

        /// <inheritdoc />
        public bool Equals(Address other) {
            return string.Equals(Street, other.Street, StringComparison.OrdinalIgnoreCase) && string.Equals(City, other.City, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(State, other.State, StringComparison.OrdinalIgnoreCase) && string.Equals(PostalCode, other.PostalCode, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(ApartmentOrSuite, other.ApartmentOrSuite, StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) {
            return obj is Address other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            unchecked {
                return Street.GetHashCode() ^ City.GetHashCode() ^ State.GetHashCode() ^ PostalCode.GetHashCode() ^ ApartmentOrSuite?.GetHashCode() ?? default;
            }
        }
    }
}