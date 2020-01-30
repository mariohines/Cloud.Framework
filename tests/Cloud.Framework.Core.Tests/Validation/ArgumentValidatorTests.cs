using System;
using System.Linq;
using Cloud.Framework.Core.Validation;
using FluentAssertions;
using Xunit;

namespace Cloud.Framework.Core.Tests.Validation
{
    public sealed class ArgumentValidatorTests
    {
        [Fact]
        public void ArgumentValidator_Begin_Returns_Null()
        {
            // act
            var validator = ArgumentValidator.Begin();
            
            // assert
            validator.Should().BeNull();
        }

        [Fact]
        public void ArgumentValidator_AddException_Returns_NonNull_ArgumentValidator()
        {
            // act
            var validator = ArgumentValidator.Begin();
            validator = ArgumentValidator.AddException(validator, new Exception("Just testing."));
            
            // assert
            validator.Should().NotBeNull();
        }

        [Fact]
        public void ArgumentValidator_GetExceptions_Returns_Exceptions()
        {
            // act
            var validator = ArgumentValidator.Begin();
            validator = ArgumentValidator.AddException(validator, new Exception("Just testing."));
            var exceptions = ArgumentValidator.GetArgumentExceptions(validator).ToList(); //doing a .ToList() to avoid multiple enumerations.
            
            // assert
            exceptions.Should().NotBeNull();
            exceptions.Should().HaveCount(1);
            exceptions.Single().Should().BeOfType<Exception>();
        }
    }
}