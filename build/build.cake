#addin "Cake.FileHelpers"
#tool "nuget:?package=xunit.runner.console"

/// args
var target = Argument("target", "default");

var rootPath = "../";
var solutionFile = rootPath + "uokoframework.sln";
var projectFiles = GetFiles(rootPath + "src/**/*.csproj");

Task("clean")
    .Does(() =>
{
    CleanDirectories(rootPath + "src/**/bin");
    CleanDirectories(rootPath + "src/**/obj");
    CleanDirectories(rootPath + "test/**/bin");
    CleanDirectories(rootPath + "test/**/obj");
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
    XUnit2(rootPath + "test/*/bin/**/*.Test.dll");
});

/// pack task
Task("pack")
    .IsDependentOn("test")
    .Does(() =>
{
    var packSetting = new DotNetCorePackSettings {
        Configuration = "Release",
        OutputDirectory = rootPath + "nupkgs/",
        IncludeSource = true,
        IncludeSymbols = true,
        NoBuild = false
    };

    foreach(var project in projectFiles){
        DotNetCorePack(project.FullPath, packSetting);
    }
});

Task("default")
    .IsDependentOn("test");

/// run task
RunTarget(target);