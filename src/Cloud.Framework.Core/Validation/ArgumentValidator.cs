using System;
using System.Collections.Generic;

namespace Cloud.Framework.Core.Validation
{
    /// <summary>
    /// Class for validating arguments fluently. Primarily for validating incoming arguments to methods or dependencies to classes.
    /// </summary>
    public sealed class ArgumentValidator
    {
        private ICollection<Exception> ExceptionSet { get; }

        private ArgumentValidator() {
            ExceptionSet = new List<Exception>();
        }

        /// <summary>
        /// Method to begin argument validation. This method call is necessary to start fluent argument validation.
        /// </summary>
        /// <example>
        /// ArgumentValidator.Begin().IsNotNull(argument, nameof(argument));
        /// </example>
        /// <remarks>
        /// This returns null because beginning validation will not start off with any errors.
        /// It's also to maintain fluency.
        /// </remarks>
        /// <returns>An ArgumentValidator object.</returns>
        public static ArgumentValidator Begin() {
            return new ArgumentValidator();
        }

        /// <summary>
        /// Method for adding exceptions while validating arguments.
        /// </summary>
        /// <param name="validator">The current instance of the <see cref="ArgumentValidator"/> class.</param>
        /// <param name="exception">The exception to add to the collection of exceptions.</param>
        /// <returns>The current instance of the <see cref="ArgumentValidator"/> class.</returns>
        public static ArgumentValidator AddException(ArgumentValidator validator, Exception exception) {
            validator.ExceptionSet.Add(exception);
            return validator;
        }

        /// <summary>
        /// Method for accessing all the exceptions that were created during validation.
        /// </summary>
        /// <param name="validator">The current instance of the <see cref="ArgumentValidator"/> class.</param>
        /// <returns>The collection of exceptions that were added via validation.</returns>
        public static IEnumerable<Exception> GetArgumentExceptions(ArgumentValidator validator) {
            return validator.ExceptionSet;
        }
    }
}