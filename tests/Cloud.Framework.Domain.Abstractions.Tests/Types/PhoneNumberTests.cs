using System;
using System.Collections.Generic;
using Cloud.Framework.Domain.Abstractions.Types;
using FluentAssertions;
using Xunit;

namespace Cloud.Framework.Domain.Abstractions.Tests.Types
{
    public sealed class PhoneNumberTests
    {
        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void PhoneNumber_Create_Returns_Expected(string sut, PhoneNumber expected) {
            // actual
            var actual = PhoneNumber.Create(sut);
            
            // assert
            actual.AreaCode.Should().Be(expected.AreaCode);
            actual.Number.Should().Be(expected.Number);
        }

        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void PhoneNumber_Implicit_Operator_Returns_Expected(string sut, PhoneNumber expected) {
            // act
            PhoneNumber actual = sut;
            
            // assert
            actual.AreaCode.Should().Be(expected.AreaCode);
            actual.Number.Should().Be(expected.Number);
        }

        [Theory]
        [InlineData("ABC1234567")]
        [InlineData("32A758")]
        [InlineData("This is just a test 1234")]
        public void PhoneNumber_Create_Throws_Exception(string sut) {
            // act
            Action action = () => PhoneNumber.Create(sut);
            
            // assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("5555555555", "5555555555")]
        [InlineData("489-555-5555", "4895555555")]
        [InlineData("480.465.5656", "4804655656")]
        public void PhoneNumber_ToString_Returns_Expected(string sut, string expected) {
            // arrange
            var phoneNumber = PhoneNumber.Create(sut);
            
            // act
            var actual = phoneNumber.ToString();
            
            // assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("5555555555", "5555555555")]
        [InlineData("489-555-5555", "4895555555")]
        [InlineData("480.465.5656", "4804655656")]
        public void PhoneNumber_StringInterpolation_Returns_Expected(string sut, string expected) {
            // arrange
            var phoneNumber = PhoneNumber.Create(sut);
            
            // act
            var actual = $"{phoneNumber}";
            
            // assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("5555555555", "(555) 555-5555")]
        [InlineData("489-555-5555", "(489) 555-5555")]
        [InlineData("480.465.5656", "(480) 465-5656")]
        public void PhoneNumber_ToDisplay_Returns_Expected(string sut, string expected) {
            // arrange
            var phoneNumber = PhoneNumber.Create(sut);
            
            // act
            var actual = phoneNumber.ToDisplay();
            
            // assert
            actual.Should().Be(expected);
        }
        
        [Theory]
        [InlineData("5555555555", "{0}-{1}-{2}", "555-555-5555")]
        [InlineData("4895555555", "{0}.{1}.{2}", "489.555.5555")]
        public void PhoneNumber_ToDisplay_WithFormat_Returns_Expected(string sut, string customFormat, string expected) {
            // arrange
            var phoneNumber = PhoneNumber.Create(sut);
            
            // act
            var actual = phoneNumber.ToDisplay(customFormat);
            
            // assert
            actual.Should().Be(expected);
        }

        public static IEnumerable<object[]> CreateTestData {
            get {
                yield return new object[] {"5555555555", new PhoneNumber("555", "5555555")};
                yield return new object[] {"(480) 465-5656", new PhoneNumber("480", "4655656")};
                yield return new object[] {"489-555-5555", new PhoneNumber("489", "5555555")};
                yield return new object[] {"489.555.5555", new PhoneNumber("489", "5555555")};
            }
        }
    }
}