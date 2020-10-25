# Cloud.Framework.MicroService.Health

## Description
Standard [SE4](https://github.com/beamly/SE4/blob/master/SE4.md) style health checks for .NET Core 3.1.

## Usage Steps
- Just follow the [normal setup](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-3.1) as documented by Microsoft.
- Substitute the use of a magic string route with the [HealthCheckConstants.Routes](./HealthCheckConstants.cs) constants.
- Use the`AddMicroServiceHealthChecks` in the `ConfigureServices` method during startup.
- Ensure that [IHealthStatusFlag](./Abstract/IHealthStatusFlag.cs) is implemented and setup as
  a _Singleton_.