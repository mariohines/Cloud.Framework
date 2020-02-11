using System;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
[GitHubActions(@"default", GitHubActionsImage.Ubuntu1604,
               GitHubActionsImage.Ubuntu1804,
               GitHubActionsImage.UbuntuLatest,
               GitHubActionsImage.WindowsLatest,
               InvokedTargets = new[] {nameof(Compile)})]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion] readonly GitVersion GitVersion;

    AbsolutePath SourceDirectory => Solution.Directory / "src";
    AbsolutePath TestsDirectory => Solution.Directory / "tests";
    AbsolutePath ArtifactsDirectory => Solution.Directory / "artifacts";

    Target Clean => _ => _
                         .Before(Restore)
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
                                                          .EnableNoRestore());
                                     });

    Target Pack => _ => _
                        // .DependsOn(Compile)
                        .Executes(() =>
                                  {
                                      foreach (var project in Solution.Projects) {
                                          // DotNetPack(_ => _
                                          //                 .SetConfiguration(Configuration)
                                          //                 .SetTitle(project.Name)
                                          //                 .SetAuthors("Mario Hines")
                                          //                 .SetOutputDirectory(ArtifactsDirectory));
                                          Console.WriteLine(project.Directory);
                                      }
                                  });
}