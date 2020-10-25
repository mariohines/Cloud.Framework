using System.Threading.Tasks;

namespace Cloud.Framework.Domain.Abstractions.Interfaces
{
    /// <summary>
    /// An interface to implement an object for event publishing.
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        ///  Method to publish events.
        /// </summary>
        /// <returns>A task.</returns>
        Task PublishAsync();
    }
}