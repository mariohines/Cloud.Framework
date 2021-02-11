# Change Log
All notable changes to the `Cloud.Framework.Domain.Abstractions` codebase.

## v1.0.0
- Move Enumeration class from `Cloud.Framework.Core` to `Cloud.Framework.Domain.Abstractions`.
- First full package release.

## v2.0.0
- Updated [AggregateRoot](./Base/AggregateChild.cs) constructors to be cleaner.
- Added `XmlIgnore` and `IgnoreDataMember` attributes to `Events` property of [AggregateRoot](./Base/AggregateChild.cs).  
- Updated `TargetFramework` to `TargetFrameworks` and included `netstandard2.0`
  and `net5.0` as optional frameworks.
- Moved most of the common project attributes to [common.props](../../common.props) file
  and imported it.
- Updated [AggregateChild](./Base/AggregateChild.cs) `Parent` property to `protected set` from `private set`.

## v2.1.0
- Added [PhoneNumber](./Types/PhoneNumber.cs) and [Address](./Types/Address.cs) value types. These are based on US Standards.