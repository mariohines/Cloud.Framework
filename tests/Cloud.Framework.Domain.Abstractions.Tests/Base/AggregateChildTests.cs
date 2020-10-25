using System;
using Cloud.Framework.Domain.Abstractions.Tests.Implementations;
using FluentAssertions;
using Xunit;

namespace Cloud.Framework.Domain.Abstractions.Tests.Base
{
    public sealed class AggregateChildTests
    {
        [Fact]
        public void AggregateChild_SetParent_Succeeds() {
            // arrange
            var sut = new ChildModel();
            
            // act
            sut.SetParent(new RootModel());
            
            // assert
            sut.Parent.Should().NotBeNull();
        }

        [Fact]
        public void AggregateChild_AddEvent_Succeeds() {
            // arrange
            var root = new RootModel();
            var sut = new ChildModel();
            sut.SetParent(root);
            
            // act
            sut.AddEvent(new SomeEvent(root));
            
            // assert
            sut.Parent.Should().NotBeNull();
            sut.Parent?.EventQueue.Should().NotBeEmpty();
        }

        [Fact]
        public void AggregateChild_AddEvent_From_GrandChild_Adds_To_Top_Succeeds() {
            // arrange
            var root = new RootModel();
            var child = new ChildModel();
            child.SetParent(root);
            var sut = new GrandChildModel();
            sut.SetParent(child);
            
            // act
            sut.AddEvent(new SomeEvent(child));
            
            // assert
            sut.Parent.Should().NotBeNull();
            root.EventQueue.Should().NotBeEmpty();
        }

        [Fact]
        public void AggregateChild_AddEvent_Throws_Exception() {
            // arrange
            var sut = new ChildModel();
            
            // act
            Action act = () => sut.AddEvent(new SomeEvent(new RootModel()));
            
            // assert
            act.Should().Throw<InvalidOperationException>();
        }
    }
}