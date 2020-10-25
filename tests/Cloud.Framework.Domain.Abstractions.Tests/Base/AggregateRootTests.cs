using Cloud.Framework.Domain.Abstractions.Tests.Implementations;
using FluentAssertions;
using Xunit;

namespace Cloud.Framework.Domain.Abstractions.Tests.Base
{
    public sealed class AggregateRootTests
    {
        [Fact]
        public void AggregateRoot_AddEvent_Succeeds() {
            // arrange
            var sut = new RootModel();
            
            // act
            sut.AddEvent(new SomeEvent(sut));
            
            // assert
            sut.EventQueue.Should().NotBeEmpty();
        }
    }
}