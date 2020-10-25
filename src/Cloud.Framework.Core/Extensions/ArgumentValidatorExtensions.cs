using System;
using System.Collections.Generic;
using System.Linq;
using Cloud.Framework.Core.Validation;

namespace Cloud.Framework.Core.Extensions
{
    /// <summary>
    /// The ArgumentValidator extensions class that is required to properly utilize the ArgumentValidator class.
    /// </summary>
    public static class ArgumentValidatorExtensions
    {
        /// <summary>
        /// Method that validates the <see cref="ArgumentValidator"/> object for errors.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        /// <exception cref="AggregateException">All the exceptions that were added during evaluation of arguments.</exception>
        public static ArgumentValidator Validate(this ArgumentValidator source) {
            var exceptions = ArgumentValidator.GetArgumentExceptions(source).ToList();
            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }

            return source;
        }

        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is null.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="argumentName">The name of the argument to be validated.</param>
        /// <typeparam name="TSource">The type of the object to be validated.</typeparam>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsNotNull<TSource>(this ArgumentValidator source, TSource argument, string argumentName)
        {
            return argument.IsNull()
                ? ArgumentValidator.AddException(source, new ArgumentNullException(argumentName))
                : source;
        }

        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is null.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="exception">The exception to be captured.</param>
        /// <typeparam name="TSource">The type of the object to be validated.</typeparam>
        /// <typeparam name="TException">The type of the exception to be captured.</typeparam>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsNotNull<TSource, TException>(this ArgumentValidator source, TSource argument, TException exception)
            where TException : Exception
        {
            return argument.IsNull()
                ? ArgumentValidator.AddException(source, exception)
                : source;
        }

        /// <summary>
        /// Method that validates if the <paramref name="arguments"/> has at least 1 item.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="arguments">The arguments to be validated.</param>
        /// <param name="argumentName">The name of the argument to be validated.</param>
        /// <typeparam name="TSource">The type of the object to be validated.</typeparam>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator MustHaveItems<TSource>(this ArgumentValidator source, IEnumerable<TSource> arguments, string argumentName)
        {
            return arguments.Any()
                ? source
                : ArgumentValidator.AddException(source, new ArgumentException("The collection must have at least 1 item.", argumentName));
        }

        /// <summary>
        /// Method that validates if the <paramref name="arguments"/> has at least 1 item.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="arguments">The arguments to be validated.</param>
        /// <param name="exception">The exception to be captured.</param>
        /// <typeparam name="TSource">The type of the object to be validated.</typeparam>
        /// <typeparam name="TException">The type of the exception to be captured.</typeparam>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator MustHaveItems<TSource, TException>(this ArgumentValidator source, IEnumerable<TSource> arguments, TException exception)
        where TException : Exception
        {
            return arguments.Any()
                ? source
                : ArgumentValidator.AddException(source, exception);
        }

        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is not null or whitespace only.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="argumentName">The name of the argument to be validated.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsNotNullOrWhitespace(this ArgumentValidator source, string argument, string argumentName)
        {
            return argument.HasValue()
                ? source
                : ArgumentValidator.AddException(source, new ArgumentException("The argument can't be null or just whitespace.", argumentName));
        }

        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is not null or whitespace only.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="exception">The exception to be captured.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsNotNullOrWhitespace<TException>(this ArgumentValidator source, string argument, TException exception)
            where TException : Exception
        {
            return argument.HasValue()
                ? source
                : ArgumentValidator.AddException(source, exception);
        }

        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is greater than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be greater than.</param>
        /// <param name="argumentName">The name of the argument to be validated.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsGreaterThan(this ArgumentValidator source, int argument, int range,
            string argumentName)
        {
            return argument > range
                ? source
                : ArgumentValidator.AddException(source, new ArgumentOutOfRangeException(argumentName));
        }

        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is greater than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be greater than.</param>
        /// <param name="argumentName">The name of the argument to be validated.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsGreaterThan(this ArgumentValidator source, long argument, long range,
            string argumentName)
        {
            return argument > range
                ? source
                : ArgumentValidator.AddException(source, new ArgumentOutOfRangeException(argumentName));
        }

        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is greater than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be greater than.</param>
        /// <param name="argumentName">The name of the argument to be validated.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsGreaterThan(this ArgumentValidator source, decimal argument, decimal range,
            string argumentName)
        {
            return argument > range
                ? source
                : ArgumentValidator.AddException(source, new ArgumentOutOfRangeException(argumentName));
        }

        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is greater than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be greater than.</param>
        /// <param name="argumentName">The name of the argument to be validated.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsGreaterThan(this ArgumentValidator source, double argument, double range,
            string argumentName)
        {
            return argument > range
                ? source
                : ArgumentValidator.AddException(source, new ArgumentOutOfRangeException(argumentName));
        }

        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is greater than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be greater than.</param>
        /// <param name="argumentName">The name of the argument to be validated.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsGreaterThan(this ArgumentValidator source, float argument, float range,
            string argumentName)
        {
            return argument > range
                ? source
                : ArgumentValidator.AddException(source, new ArgumentOutOfRangeException(argumentName));
        }
        
        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is greater than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be greater than.</param>
        /// <param name="exception">The exception to be captured.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsGreaterThan<TException>(this ArgumentValidator source, int argument, int range,
            TException exception) where TException : Exception
        {
            return argument > range
                ? source
                : ArgumentValidator.AddException(source, exception);
        }
        
        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is greater than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be greater than.</param>
        /// <param name="exception">The exception to be captured.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsGreaterThan<TException>(this ArgumentValidator source, long argument, long range,
            TException exception) where TException : Exception
        {
            return argument > range
                ? source
                : ArgumentValidator.AddException(source, exception);
        }
        
        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is greater than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be greater than.</param>
        /// <param name="exception">The exception to be captured.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsGreaterThan<TException>(this ArgumentValidator source, double argument, double range,
            TException exception) where TException : Exception
        {
            return argument > range
                ? source
                : ArgumentValidator.AddException(source, exception);
        }
        
        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is greater than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be greater than.</param>
        /// <param name="exception">The exception to be captured.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsGreaterThan<TException>(this ArgumentValidator source, decimal argument, decimal range,
            TException exception) where TException : Exception
        {
            return argument > range
                ? source
                : ArgumentValidator.AddException(source, exception);
        }
        
        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is greater than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be greater than.</param>
        /// <param name="exception">The exception to be captured.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsGreaterThan<TException>(this ArgumentValidator source, float argument, float range,
            TException exception) where TException : Exception
        {
            return argument > range
                ? source
                : ArgumentValidator.AddException(source, exception);
        }

        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is less than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be less than.</param>
        /// <param name="argumentName">The name of the argument to be validated.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsLessThan(this ArgumentValidator source, int argument, int range,
            string argumentName)
        {
            return argument < range
                ? source
                : ArgumentValidator.AddException(source, new ArgumentOutOfRangeException(argumentName));
        }

        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is less than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be less than.</param>
        /// <param name="argumentName">The name of the argument to be validated.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsLessThan(this ArgumentValidator source, long argument, long range,
            string argumentName)
        {
            return argument < range
                ? source
                : ArgumentValidator.AddException(source, new ArgumentOutOfRangeException(argumentName));
        }

        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is less than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be less than.</param>
        /// <param name="argumentName">The name of the argument to be validated.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsLessThan(this ArgumentValidator source, decimal argument, decimal range,
            string argumentName)
        {
            return argument < range
                ? source
                : ArgumentValidator.AddException(source, new ArgumentOutOfRangeException(argumentName));
        }

        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is less than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be less than.</param>
        /// <param name="argumentName">The name of the argument to be validated.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsLessThan(this ArgumentValidator source, double argument, double range,
            string argumentName)
        {
            return argument < range
                ? source
                : ArgumentValidator.AddException(source, new ArgumentOutOfRangeException(argumentName));
        }

        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is less than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be less than.</param>
        /// <param name="argumentName">The name of the argument to be validated.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsLessThan(this ArgumentValidator source, float argument, float range,
            string argumentName)
        {
            return argument < range
                ? source
                : ArgumentValidator.AddException(source, new ArgumentOutOfRangeException(argumentName));
        }
        
        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is less than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be less than.</param>
        /// <param name="exception">The exception to be captured.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsLessThan<TException>(this ArgumentValidator source, int argument, int range,
            TException exception) where TException : Exception
        {
            return argument < range
                ? source
                : ArgumentValidator.AddException(source, exception);
        }
        
        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is less than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be less than.</param>
        /// <param name="exception">The exception to be captured.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsLessThan<TException>(this ArgumentValidator source, long argument, long range,
            TException exception) where TException : Exception
        {
            return argument < range
                ? source
                : ArgumentValidator.AddException(source, exception);
        }
        
        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is less than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be less than.</param>
        /// <param name="exception">The exception to be captured.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsLessThan<TException>(this ArgumentValidator source, double argument, double range,
            TException exception) where TException : Exception
        {
            return argument < range
                ? source
                : ArgumentValidator.AddException(source, exception);
        }
        
        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is less than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be less than.</param>
        /// <param name="exception">The exception to be captured.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsLessThan<TException>(this ArgumentValidator source, decimal argument, decimal range,
            TException exception) where TException : Exception
        {
            return argument < range
                ? source
                : ArgumentValidator.AddException(source, exception);
        }
        
        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is less than the <paramref name="range"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="range">The value that the <paramref name="argument"/> must be less than.</param>
        /// <param name="exception">The exception to be captured.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsLessThan<TException>(this ArgumentValidator source, float argument, float range,
            TException exception) where TException : Exception
        {
            return argument < range
                ? source
                : ArgumentValidator.AddException(source, exception);
        }

        /// <summary>
        /// Method that validates if the <paramref name="argument"/> is of the same type as <paramref name="argumentType"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="argumentType"></param>
        /// <param name="argumentName">The name of the argument to be validated.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsOfType(this ArgumentValidator source, object argument, Type argumentType, string argumentName)
        {
            return argument.GetType() == argumentType
                ? source
                : ArgumentValidator.AddException(source, new ArgumentException(argumentName));
        }
        
        /// <summary>
        /// Method that validates if the <paramref name="argument"/> has a base type of <paramref name="argumentType"/>.
        /// </summary>
        /// <param name="source">The <see cref="ArgumentValidator"/> this method is executed on.</param>
        /// <param name="argument">The argument to be validated.</param>
        /// <param name="argumentType"></param>
        /// <param name="argumentName">The name of the argument to be validated.</param>
        /// <returns>An <see cref="ArgumentValidator"/> object.</returns>
        public static ArgumentValidator IsOfBaseType(this ArgumentValidator source, object argument, Type argumentType, string argumentName)
        {
            return argument.GetType().BaseType == argumentType
                ? source
                : ArgumentValidator.AddException(source, new ArgumentException(argumentName));
        }
    }
}