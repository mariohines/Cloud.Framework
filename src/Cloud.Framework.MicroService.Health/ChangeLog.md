# Change Log
All notable changes to the `Cloud.Framework.MicroService.Health` codebase.

## v0.1.0
- Initial Release

## v1.0.0
- First full package release.

## v2.0.0
- Updated [HealthCheckResource](./Models/HealthCheckResource.cs) class to be `sealed`.
- Updated [HealthCheckTestResult](./Models/HealthCheckResource.cs) class to be `sealed`.
- Updated `TargetFramework` to `TargetFrameworks` and included `netstandard2.0`
  and `net5.0` as optional frameworks.
- Moved most of the common project attributes to [common.props](../../common.props) file
and imported it.