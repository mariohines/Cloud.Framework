using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter("API Key for publishing packages to GitHub Package Repository. This should be handled by the runner environment.", Name = "Token")]
    readonly string GitHubToken;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion] readonly GitVersion GitVersion;

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
                                                           .SetProjectFile(Solution));
                                     });

    Target Compile => _ => _
                           .DependsOn(Restore)
                           .Executes(() =>
                                     {
                                         DotNetBuild(c => c
                                                          .SetProjectFile(Solution)
                                                          .SetConfiguration(Configuration)
                                                          .SetAssemblyVersion(GitVersion.AssemblySemVer)
                                                          .SetFileVersion(GitVersion.AssemblySemFileVer)
                                                          .SetInformationalVersion(GitVersion.InformationalVersion)
                                                          .SetNoRestore(InvokedTargets.Contains(Restore)));
                                     });

    Target UnitTests => _ => _
                             .DependsOn(Compile)
                             .Executes(() =>
                                       {
                                           DotNetTest(c => c
                                                           .SetProcessWorkingDirectory(TestsDirectory)
                                                           .SetProjectFile(Solution)
                                                           .SetNoBuild(InvokedTargets.Contains(Compile))
                                                           .EnableCollectCoverage()
                                                           .SetCoverletOutput(CoverageOutputDirectory / "coverage.info")
                                                           .SetCoverletOutputFormat(CoverletOutputFormat.lcov));
                                       });

    Target Pack => _ => _
                        .DependsOn(UnitTests)
                        .Executes(() =>
                                  {
                                      Solution.Projects
                                              .ForEach(project =>
                                                       {
                                                           var changeLogFile = project.Directory / "ChangeLog.md";
                                                           DotNetPack(c => c
                                                                           .SetConfiguration(Configuration)
                                                                           .SetProcessWorkingDirectory(project.Directory)
                                                                           .SetOutputDirectory(ArtifactsDirectory)
                                                                           .SetSymbolPackageFormat(DotNetSymbolPackageFormat.snupkg)
                                                                           .SetVersion(GitVersion.MajorMinorPatch)
                                                                           .EnableIncludeSymbols());
                                                       });
                                  });

    Target Push => _ => _
                        .ProceedAfterFailure()
                        .DependsOn(Pack)
                        .Consumes(Pack)
                        .Requires(() => GitHubToken)
                        .Executes(() =>
                                  {
                                      DotNetNuGetPush(s => s
                                                           .SetSource(PackagePushSource)
                                                           .EnableSkipDuplicate()
                                                           .SetApiKey(GitHubToken)
                                                           .CombineWith(ArtifactsDirectory.GlobFiles(PackageFiles).NotEmpty(), (c, v) =>
                                                                            c.SetTargetPath(v)));
                                  });
}
