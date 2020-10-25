# Cloud.Framework.Domain.Abstractions

## Description
Basic core library that holds domain drive design (DDD) abstractions.

### Important Classes
- [Enumeration](./Base/Enumeration.cs): Abstract class for DDD enumerations with functionality.
- [AggregateRoot](./Base/AggregateRoot.cs): Abstract class for DDD models.
- [AggregateChild](./Base/AggregateChild.cs): Abstract class for _child_ DDD models.
- [IAggregateTracker](./Interfaces/IAggregateTracker.cs): Interface for an implementation to track and manage changes across a DDD model.
- [IEventPublisher](./Interfaces/IEventPublisher.cs): Interface for an implementation to publish events from a DDD model.
- [IEvent](./Interfaces/IEvent.cs): Interface for an implementation of an event. This is to abstract away dependencies on other frameworks.
