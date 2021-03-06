using System;
using System.Collections;
using System.Collections.Generic;
using Cloud.Framework.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace Cloud.Framework.Core.Tests.Extensions
{
    public sealed class StringExtensionsTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData("Test", true)]
        [InlineData("   ", false)]
        public void StringExtensions_HasValue_Returns_Expected(string sut, bool expected)
        {
            // act
            var actual = sut.HasValue();
            
            // assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void StringExtensions_EmptyIfNull_Returns_Expected() {
            // arrange
            string sut = null;
            
            // act
            var actual = sut.EmptyIfNull();
            
            // assert
            actual.Should().Be(string.Empty);
        }

        [Theory]
        [InlineData("true", true)]
        [InlineData("false", false)]
        [InlineData("True", true)]
        [InlineData("False", false)]
        [InlineData("TRUE", true)]
        [InlineData("FALSE", false)]
        [InlineData("something", false)]
        [InlineData("nothing", false)]
        public void StringExtensions_ToBoolean_Returns_Expected(string sut, bool expected)
        {
            // act
            var actual = sut.ToBoolean();
            
            // assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("Hel", "lo", "Hello")]
        [InlineData("Good", "Bye", "GoodBye")]
        [InlineData("Good ", "Night", "Good Night")]
        [InlineData("Suck", " Up", "Suck Up")]
        [InlineData("Just a ", "test", "Just a test")]
        public void StringExtensions_Append_Returns_Expected(string sut, string appendedValue, string expected) {
            // act
            var actual = sut.Append(appendedValue);
            
            // assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void StringExtensions_Append_Chained_Append_Returns_Expected() {
            // assert
            const string sut = "This";
            const string expected = "This is just a test.";

            // act
            var actual = sut.Append(" is")
                            .Append(" just")
                            .Append(" a")
                            .Append(" test")
                            .Append(".");
            
            // assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("Just A Test", 1, 100, true)]
        [InlineData("Just A Test", 1, 2, false)]
        [InlineData("Just A Test", 1, 5, false)]
        [InlineData("Just A Test", 1, 9, false)]
        [InlineData("Just A Test", 8, 9, false)]
        [InlineData("Just A Test", 9, 11, true)]
        [InlineData("Just A Test", 11, 11, true)]
        public void StringExtensions_IsLengthBetween_Returns_Expected(string sut, int min, int max, bool expected)
        {
            // act
            var actual = sut.IsLengthBetween(min, max);
            
            // assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("Hello {0}", new object[] {"World"}, "Hello World")]
        [InlineData("{0} {1}", new object[] {"Hello", "World"}, "Hello World")]
        [InlineData("{0} {1} {2}", new object[] {"A", "B", "C"}, "A B C")]
        [InlineData("{0} {1} {2} {3}", new object[] {"A", "B", "C", 1}, "A B C 1")]
        [InlineData("{0} {1} {2} {3} {4}", new object[] {"A", "B", "C", 1, 2}, "A B C 1 2")]
        [InlineData("{0} {1} {2} {3} {4} {5}", new object[] {"A", "B", "C", 1, 2, 3}, "A B C 1 2 3")]
        public void StringExtensions_FormatWith_Returns_Expected(string sut, object[] parameters, string expected)
        {
            // act
            var actual = sut.FormatWith(parameters);
            
            // assert
            actual.Should().NotBeNullOrWhiteSpace();
            actual.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(GetDateFromStringTestData))]
        public void StringExtensions_GetDateFromString_Returns_Expected(string sut, DateTime expected)
        {
            // act
            var actual = sut.GetDateFromString();
            
            // assert
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("Test", new[] {(string)null, "Hello"}, "Test")]
        [InlineData((string)null, new[] {"Hello", "Just in case"}, "Hello")]
        public void StringExtensions_Coalesce_Returns_Expected(string sut, string[] parameters, string expected) {
            // act
            var actual = sut.Coalesce(parameters);
            
            // assert
            actual.Should().Be(expected);
        }

        private sealed class GetDateFromStringTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {"3/12/19", new DateTime(2019, 3, 12)};
                yield return new object[] {"3/12/2019", new DateTime(2019, 3, 12)};
                yield return new object[] {"03/12/19", new DateTime(2019, 3, 12)};
                yield return new object[] {"3/12/19 05:20:00", new DateTime(2019, 3, 12, 5, 20, 0)};
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}