using System;
using System.Collections.Generic;
using Cloud.Framework.Domain.Abstractions.Types;
using FluentAssertions;
using Xunit;

namespace Cloud.Framework.Domain.Abstractions.Tests.Types
{
    public sealed class AddressTests
    {
        [Theory]
        [MemberData(nameof(AddressCreateData))]
        public void Address_Create_Returns_Expected(string street, string city, string state, string postalCode, string apartmentOrSuite, Address expected) {
            // act
            var actual = Address.Create(street, city, state, postalCode, apartmentOrSuite);

            // assert
            actual.Street.Should().Be(expected.Street);
            actual.City.Should().Be(expected.City);
            actual.State.Should().Be(expected.State);
            actual.PostalCode.Should().Be(expected.PostalCode);
            actual.ApartmentOrSuite.Should().Be(expected.ApartmentOrSuite);
        }

        [Theory]
        [MemberData(nameof(AddressThrowsExceptionData))]
        public void Address_Create_Throws_Exception(string street, string city, string state, string postalCode) {
            // act
            Action action = () => { Address.Create(street, city, state, postalCode); };

            // assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(AddressOperatorData))]
        public void Address_Implicit_Operator_Returns_Expected(Address sut, string expected) {
            // act
            string actual = sut;

            // assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(AddressEqualsData))]
        public void Address_Equals_Returns_Expected(Address addressOne, Address addressTwo, bool expected) {
            // act
            var actual = addressOne.Equals(addressTwo);

            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable<object[]> AddressCreateData {
            get {
                yield return new object[] {"1 W. One Street", "Anchorage", "AK", "11111", null, new Address("1 W. One Street", "Anchorage", "AK", "11111")};
                yield return new object[] {"2 W. One Street", "Anchorage", "AK", "11111", "4567", new Address("2 W. One Street", "Anchorage", "AK", "11111", "4567")};
                yield return new object[] {"3 W. One Street", "Anchorage", "AK", "11111", null, new Address("3 W. One Street", "Anchorage", "AK", "11111")};
                yield return new object[] {"4 W. One Street", "Anchorage", "AK", "11111", "0123", new Address("4 W. One Street", "Anchorage", "AK", "11111", "0123")};
            }
        }

        public static IEnumerable<object[]> AddressThrowsExceptionData {
            get {
                yield return new object[] {null, "Anchorage", "AK", "11111"};
                yield return new object[] {"1 W. One Street", null, "AK", "11111"};
                yield return new object[] {"1 W. One Street", "Anchorage", null, "11111"};
                yield return new object[] {"1 W. One Street", "Anchorage", "AK", null};
            }
        }

        public static IEnumerable<object[]> AddressOperatorData {
            get {
                yield return new object[] {new Address("1 W. One Street", "Anchorage", "AK", "11111"), "1 W. One Street, Anchorage, AK 11111"};
                yield return new object[] {new Address("2 W. One Street", "Anchorage", "AK", "11111", "#223"), "2 W. One Street #223, Anchorage, AK 11111"};
                yield return new object[] {new Address("3 W. One Street", "Anchorage", "AK", "11111"), "3 W. One Street, Anchorage, AK 11111"};
                yield return new object[] {new Address("4 W. One Street", "Anchorage", "AK", "11111", "Suite 101"), "4 W. One Street Suite 101, Anchorage, AK 11111"};
            }
        }

        public static IEnumerable<object[]> AddressEqualsData {
            get {
                yield return new object[] {new Address("1 W. One Street", "Anchorage", "AK", "11111"), new Address("1 W. One Street", "Anchorage", "AK", "11111"), true};
                yield return new object[] {new Address("2 W. One Street", "Anchorage", "AK", "11111"), new Address("3 W. One Street", "Anchorage", "AK", "11111"), false};
                yield return new object[] {new Address("4 w. one street", "Anchorage", "AK", "11111"), new Address("4 W. One Street", "Anchorage", "AK", "11111"), true};
                yield return new object[] {new Address("5 W. One Street", "Anchorage", "AK", "11111"), new Address("1 E. One Street", "Anchorage", "AK", "11111"), false};
            }
        }
    }
}