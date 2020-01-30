using System.Net;
using Cloud.Framework.Core.Attributes;
using FluentAssertions;
using Xunit;

namespace Cloud.Framework.Core.Tests.Attributes
{
    public sealed class ExceptionResponseStatusCodeAttributeTests
    {
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.NoContent)]
        [InlineData(HttpStatusCode.Conflict)]
        [InlineData(HttpStatusCode.BadRequest)]
        public void Implicit_Conversion_To_Int_Success(HttpStatusCode statusCode)
        {
            // arrange
            var expected = (int) statusCode;
            
            // act
            var attribute = new ExceptionResponseStatusCodeAttribute(statusCode);
            int actual = attribute;

            // assert
            actual.Should().Be(expected);
        }
    }
}