using build;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.ChangeLog.ChangelogTasks;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
[GitHubActions(@"main",
               GitHubActionsImage.UbuntuLatest,
               GitHubActionsImage.MacOsLatest,
               AutoGenerate = false,
               On = new[] {GitHubActionsTrigger.Push},
               InvokedTargets = new[] {nameof(Push)},
               ImportGitHubTokenAs = nameof(_gitHubToken))]
class Build : NukeBuild
{
    public static int Main() => Execute<Build>();

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)", Name = nameof(Configuration))]
    readonly Configuration _configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter("API Key for publishing packages to GitHub Package Repository. This should be handled by the runner environment.", Name = "Token")]
    readonly string _gitHubToken;

    [Required] [Solution] readonly Solution _solution;
    [Required] [GitRepository] readonly GitRepository _gitRepository;
    [Required] [GitVersion(Framework = "netcoreapp3.0")] readonly GitVersion _gitVersion;

    static AbsolutePath SourceDirectory => RootDirectory / "src";
    static AbsolutePath TestsDirectory => RootDirectory / "tests";
    static AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    static AbsolutePath CoverageOutputDirectory => ArtifactsDirectory / "coverage-results";

    const string PackagePushSource = "https://nuget.pkg.github.com/mariohines/index.json";
    const string PackageFiles = "*.nupkg";

    Target Clean => _ => _
                               .Executes(() =>
                                         {
                                             SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
                                             TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
                                             EnsureCleanDirectory(ArtifactsDirectory);
                                         });

    Target Restore => _ => _
                           .DependsOn(Clean)
                           .Executes(() =>
                                     {
                                         DotNetRestore(c => c
                                                           .SetProjectFile(_solution));
                                     });

    Target Compile => _ => _
                           .DependsOn(Restore)
                           .Executes(() =>
                                     {
                                         DotNetBuild(c => c
                                                          .SetProjectFile(_solution)
                                                          .SetConfiguration(_configuration)
                                                          .SetAssemblyVersion(_gitVersion.AssemblySemVer)
                                                          .SetFileVersion(_gitVersion.AssemblySemFileVer)
                                                          .SetInformationalVersion(_gitVersion.InformationalVersion)
                                                          .SetNoRestore(InvokedTargets.Contains(Restore)));
                                     });

    Target UnitTests => _ => _
                             .DependsOn(Compile)
                             .Executes(() =>
                                       {
                                           DotNetTest(c => c
                                                           .SetProcessWorkingDirectory(TestsDirectory)
                                                           .SetProjectFile(_solution)
                                                           .SetNoBuild(InvokedTargets.Contains(Compile))
                                                           .EnableCollectCoverage()
                                                           .SetCoverletOutput(CoverageOutputDirectory / "coverage.info")
                                                           .SetCoverletOutputFormat(CoverletOutputFormat.lcov));
                                       });

    Target Pack => _ => _
                        .DependsOn(UnitTests)
                        .Executes(() =>
                                  {
                                      _solution.Projects
                                              .ForEach(project =>
                                                       {
                                                           var changeLogFile = project.Directory / "ChangeLog.md";
                                                           DotNetPack(c => c
                                                                           .SetConfiguration(_configuration)
                                                                           .SetProcessWorkingDirectory(project.Directory)
                                                                           .SetOutputDirectory(ArtifactsDirectory)
                                                                           .SetSymbolPackageFormat(DotNetSymbolPackageFormat.snupkg)
                                                                           .SetVersion(_gitVersion.MajorMinorPatch)
                                                                           .SetPackageReleaseNotes(GetNuGetReleaseNotes(changeLogFile, _gitRepository))
                                                                           .EnableIncludeSymbols());
                                                       });
                                  });

    Target Push => _ => _
                        .ProceedAfterFailure()
                        .DependsOn(Pack)
                        .Consumes(Pack)
                        .Requires(() => _gitHubToken)
                        .Executes(() =>
                                  {
                                      DotNetNuGetPush(s => s
                                                           .SetSource(PackagePushSource)
                                                           .EnableSkipDuplicate()
                                                           .SetApiKey(_gitHubToken)
                                                           .CombineWith(ArtifactsDirectory.GlobFiles(PackageFiles).NotEmpty(), (c, v) =>
                                                                            c.SetTargetPath(v)));
                                  });
}