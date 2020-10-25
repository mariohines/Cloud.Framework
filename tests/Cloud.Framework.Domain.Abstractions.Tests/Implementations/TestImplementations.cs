using Cloud.Framework.Domain.Abstractions.Base;
using Cloud.Framework.Domain.Abstractions.Interfaces;

namespace Cloud.Framework.Domain.Abstractions.Tests.Implementations
{
    public sealed class RootModel : AggregateRoot
    {
    }

    public sealed class ChildModel : AggregateChild<RootModel>
    {
    }

    public sealed class GrandChildModel : AggregateChild<ChildModel>
    {
    }

    public readonly struct SomeEvent : IEvent
    {
        public AggregateRoot Model { get; }

        public SomeEvent(AggregateRoot model) {
            Model = model;
        }
    }

    public sealed class CommonEnumeration : Enumeration<int>
    {
        public static CommonEnumeration No = new CommonEnumeration(0, nameof(No));
        public static CommonEnumeration Yes = new CommonEnumeration(1, nameof(Yes));
        public static CommonEnumeration Maybe = new CommonEnumeration(2, nameof(Maybe));
        
        private CommonEnumeration(int id, string name) : base(id, name) { }
    }
}