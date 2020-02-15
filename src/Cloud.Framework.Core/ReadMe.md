# Cloud.Framework.Core

## Description
Basic core library that holds general use functionality that can/should be used across the entirety of a solution.

### Important Classes
- [Enumeration](./Abstract/Enumeration.cs): Abstract class for DDD enumerations with functionality.
- [ExceptionResponseStatusCodeAttribute](./Attributes/ExceptionResponseStatusCodeAttribute.cs): Attribute to specify the HTTP status code that should be thrown on custom exceptions.
- [ArgumentValidator](./Validation/ArgumentValidator.cs): A fluent static class that can be used for argument/parameter validation. Should primarily be used in constructors.