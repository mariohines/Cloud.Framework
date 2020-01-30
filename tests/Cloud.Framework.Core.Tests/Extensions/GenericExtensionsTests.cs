using Cloud.Framework.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace Cloud.Framework.Core.Tests.Extensions
{
    public sealed class GenericExtensionsTests 
    {
        [Theory]
        [InlineData("", true)]
        [InlineData("Test", false)]
        [InlineData("   ", true)]
        public void GenericExtensions_IsNull_Returns_Expected(object sut, bool expected)
        {
            // act
            var actual = sut.IsNull();
            
            // assert
            actual.Should().Be(expected);
        }
    }
}