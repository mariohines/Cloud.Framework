using System.Collections.Generic;
using Cloud.Framework.Domain.Abstractions.Tests.Implementations;
using FluentAssertions;
using Xunit;

namespace Cloud.Framework.Domain.Abstractions.Tests.Base
{
    public sealed class EnumerationTests
    {
        [Fact]
        public void Enumeration_ToString_Returns_Success() {
            // arrange
            var sut = CommonEnumeration.Yes;
            
            // act
            var result = sut.ToString();
            
            // arrange
            result.Should().Be(nameof(CommonEnumeration.Yes));
        }

        [Fact]
        public void Enumeration_GetAll_Returns_Success() {
            // arrange
            var expectedResult = new List<CommonEnumeration> {CommonEnumeration.No, CommonEnumeration.Yes, CommonEnumeration.Maybe};
            
            // act
            var sut = CommonEnumeration.GetAll<CommonEnumeration>();
            
            // assert
            sut.Should().BeEquivalentTo(expectedResult);
        }
    }
}