using System;
using System.Net;

namespace Cloud.Framework.Core.Attributes
{
    /// <summary>
    /// Attribute to set what the exceptions <see cref="HttpStatusCode"/> should be in the response.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ExceptionResponseStatusCodeAttribute : Attribute
    {
        private readonly HttpStatusCode _statusCode;

        /// <summary>
        /// Constructor to set the <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/> the response should have on the decorated exception.</param>
        public ExceptionResponseStatusCodeAttribute(HttpStatusCode statusCode) {
            _statusCode = statusCode;
        }

        /// <summary>
        /// Implicit operator to allow the attribute to be used as an <see cref="Int32"/>.
        /// </summary>
        /// <param name="attribute">The <see cref="ExceptionResponseStatusCodeAttribute"/> being acted upon.</param>
        /// <returns>The integer value of a <see cref="HttpStatusCode"/>.</returns>
        public static implicit operator int(ExceptionResponseStatusCodeAttribute attribute) {
            return (int) attribute._statusCode;
        }
    }
}