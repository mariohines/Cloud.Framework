using System;
using System.Collections.Generic;
using System.Linq;
using Cloud.Framework.Core.Extensions;
using Cloud.Framework.Core.Validation;
using FluentAssertions;
using Xunit;

namespace Cloud.Framework.Core.Tests.Extensions
{
    public class ArgumentValidationExtensionsTests
    {
        
    }public sealed class ArgumentValidatorExtensionsTests
    {
        [Fact]
        public void ArgumentValidator_Validate_Returns_Null()
        {
            // act
            var validator = ArgumentValidator.Begin().Validate();
            
            // assert
            validator.Should().BeNull();
        }

        [Fact]
        public void ArgumentValidator_Validate_Throws_Error()
        {
            // arr
            Action action = () => ArgumentValidator.Begin()
                                                   .IsNotNull(default(string), @"argument")
                                                   .Validate();
            
            // act | assert
            action.Should().Throw<AggregateException>()
                .WithInnerException<ArgumentNullException>();
        }
        
        [Theory]
        [InlineData((string)null)]
        [InlineData("")]
        [InlineData("     ")]
        public void ArgumentValidator_IsNotNull_Throws_Error_With_String(string argument)
        {
            // arr
            Action action = () => ArgumentValidator.Begin()
                .IsNotNull(argument, nameof(argument))
                .Validate();
            
            // act | assert
            action.Should().Throw<AggregateException>()
                .WithInnerException<ArgumentNullException>();
        }

        [Fact]
        public void ArgumentValidator_MustHaveItems_Returns_Null()
        {
            // arr
            var collection = new List<int> {1, 2, 3};
            
            // act
            var validator = ArgumentValidator.Begin()
                .MustHaveItems(collection, nameof(collection))
                .Validate();
            
            // assert
            validator.Should().BeNull();
        }

        [Fact]
        public void ArgumentValidator_MustHaveItems_Throws_Error_With_Empty_Collection()
        {
            // arr
            var collection = Enumerable.Empty<int>();
            
            // act
            Action action = () => ArgumentValidator.Begin()
                .MustHaveItems(collection, nameof(collection))
                .Validate();
            
            // assert
            action.Should().Throw<AggregateException>()
                .WithInnerException<ArgumentException>();
        }
        
        [Theory]
        [InlineData((string)null)]
        [InlineData("")]
        [InlineData("     ")]
        public void ArgumentValidator_IsNotNullOrWhitespace_Throws_Error_With_String(string argument)
        {
            // arr
            Action action = () => ArgumentValidator.Begin()
                .IsNotNullOrWhitespace(argument, nameof(argument))
                .Validate();
            
            // act | assert
            action.Should().Throw<AggregateException>()
                .WithInnerException<ArgumentException>();
        }

        [Theory]
        [InlineData((string)null)]
        [InlineData("")]
        [InlineData("     ")]
        public void ArgumentValidator_IsNotNullOrWhitespace_Throw_Custom_Error_With_String(string argument)
        {
            // arr
            Action action = () => ArgumentValidator.Begin()
                .IsNotNullOrWhitespace(argument, new CustomTestException())
                .Validate();
            
            // act | assert
            action.Should().Throw<AggregateException>()
                .WithInnerException<CustomTestException>();
        }

        private sealed class CustomTestException : Exception
        {
            
        }
    }
}