using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.ChangeLog.ChangelogTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
[GitHubActions(@"main",
               GitHubActionsImage.Ubuntu1804,
               GitHubActionsImage.MacOsLatest,
               AutoGenerate = true,
               On = new[] {GitHubActionsTrigger.Push},
               InvokedTargets = new[] {nameof(Push)},
               ImportGitHubTokenAs = "GITHUBTOKEN")]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>();

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter("API Key for publishing packages to GitHub Package Repository. This should be handled by the runner environment.")]
    readonly string GitHubToken;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion(Framework = "netcoreapp2.1")] readonly GitVersion GitVersion;

    static AbsolutePath SourceDirectory => RootDirectory / "src";
    static AbsolutePath TestsDirectory => RootDirectory / "tests";
    static AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    const string Author = "Mario S. Hines";
    const string ProjectUrl = "https://githumb.com/mariohines/Cloud.Framework";
    const string CopyRight = "Gigatech Software Consulting";
    const string ChangeLogFile = "ChangeLog.md";
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
                                         DotNetRestore(_ => _
                                                           .SetProjectFile(Solution));
                                     });

    Target Compile => _ => _
                           .DependsOn(Restore)
                           .Executes(() =>
                                     {
                                         DotNetBuild(_ => _
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
                                           DotNetTest(_ => _
                                                           .SetWorkingDirectory(TestsDirectory)
                                                           .SetProjectFile(Solution)
                                                           .SetNoBuild(InvokedTargets.Contains(Compile)));
                                       });

    Target Pack => _ => _
                        .DependsOn(UnitTests)
                        .Executes(() =>
                                  {
                                      Solution.Projects
                                              .ForEach(project =>
                                                       {
                                                           var currentChangeLogFile = project.Directory / ChangeLogFile;
                                                           DotNetPack(_ => _
                                                                           .SetConfiguration(Configuration)
                                                                           .SetWorkingDirectory(project.Directory)
                                                                           .SetOutputDirectory(ArtifactsDirectory)
                                                                           .SetPackageProjectUrl(ProjectUrl)
                                                                           .SetSymbolPackageFormat(DotNetSymbolPackageFormat.snupkg)
                                                                           .SetAuthors(Author)
                                                                           .SetTitle(project.Name)
                                                                           .SetCopyright(CopyRight)
                                                                           .SetDescription(project.Name)
                                                                           .SetPackageReleaseNotes(GetNuGetReleaseNotes(currentChangeLogFile, GitRepository))
                                                                           .SetVersion(GitVersion.NuGetVersionV2)
                                                                           .EnableIncludeSymbols());
                                                       });
                                  });

    Target Push => _ => _
                        .DependsOn(Pack)
                        .Requires(() => GitHubToken)
                        .Executes(() =>
                                  {
                                      DotNetNuGetPush(s => s
                                                           .SetSource(PackagePushSource)
                                                           .SetApiKey(GitHubToken)
                                                           .CombineWith(ArtifactsDirectory.GlobFiles(PackageFiles).NotEmpty(), (s, v) => s.SetTargetPath(v)));
                                  });
}