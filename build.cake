#addin "Cake.FileHelpers"
#tool "nuget:?package=xunit.runner.console"

/// args
var target = Argument("target", "default");

var solutionFile="./uokoframework.sln";
var projects = GetFiles("./src/**/*.csproj");

Task("clean")
    .Does(() =>
{
    CleanDirectories("./src/**/bin");
    CleanDirectories("./src/**/obj");
    CleanDirectories("./test/**/bin");
    CleanDirectories("./test/**/obj");
});

/// nuget task
Task("restore")
    .Does(() =>
{
    DotNetCoreRestore(solutionFile);
});

/// build task
Task("build")
    .IsDependentOn("clean")
    .IsDependentOn("restore")
    .Does(() =>
{
    // MSBuild(solutionFile,new MSBuildSettings {
	// 	Verbosity = Verbosity.Minimal,
	// 	ToolVersion = MSBuildToolVersion.VS2017,
	// 	Configuration = "Release",
    // });
    DotNetCoreBuild(solutionFile);
});

/// test task
Task("test")
    .IsDependentOn("build")
    .Does(() =>
{
    XUnit2("./test/*/bin/**/*.Test.dll");
});

/// pack task
Task("pack")
    .IsDependentOn("test")
    .Does(() =>
{
    foreach(var project in projects){
        // MSBuild(project, new MSBuildSettings {
        //     Verbosity = Verbosity.Minimal,
        //     ToolVersion = MSBuildToolVersion.VS2017,
        //     Configuration = "Release",
        //     ArgumentCustomization = args => args.Append("/target:pack")
        // });

        DotNetCorePack(project.FullPath, new DotNetCorePackSettings {
            Configuration = "Release"
        });
        
    }
});

Task("default")
    .IsDependentOn("pack");

/// run task
RunTarget(target);