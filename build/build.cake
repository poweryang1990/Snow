#addin "Cake.FileHelpers"
#tool "nuget:?package=xunit.runner.console"

/// args
var target = Argument("target", "default");

var solutionFile="../uokoframework.sln";
var projects = GetFiles("../src/**/*.csproj");

Task("clean")
    .Does(() =>
{
    CleanDirectories("../src/**/bin");
    CleanDirectories("../src/**/obj");
    CleanDirectories("../test/**/bin");
    CleanDirectories("../test/**/obj");
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
    DotNetCoreBuild(solutionFile);
});

/// test task
Task("test")
    .IsDependentOn("build")
    .Does(() =>
{
    XUnit2("../test/*/bin/**/*.Test.dll");
});

/// pack task
Task("pack")
    .IsDependentOn("test")
    .Does(() =>
{
    var packSetting = new DotNetCorePackSettings {
        Configuration = "Release",
        OutputDirectory = "../.nuget/",
        IncludeSource = true,
        IncludeSymbols = true,
        NoBuild = false
    };

    foreach(var project in projects){
        DotNetCorePack(project.FullPath, packSetting);
    }
});

Task("default")
    .IsDependentOn("test");

/// run task
RunTarget(target);