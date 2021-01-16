# Change Log
All notable changes to the `Cloud.Framework.Persistence.Abstractions` codebase.

## v0.1.0
- Initial Release

## v1.0.0
- First full package release.

## v2.0.0
- Updated `TargetFramework` to `TargetFrameworks` and included `netstandard2.0`
  and `net5.0` as optional frameworks.
- Moved most of the common project attributes to [common.props](../../common.props) file
  and imported it.
- Updated [DbQuery](./Sql.Base/DbQuery.cs) to allow passing in a transaction.
- Added a method to create a non-asynchronous transaction to [IDbContext](./Sql.Interfaces/IDbContext.cs).
- Added a method to get the current transaction to [IDbContext](./Sql.Interfaces/IDbContext.cs).