//////////////////////////////////////////////////////////////////////
// TOOLS
//////////////////////////////////////////////////////////////////////
#tool "nuget:?package=GitVersion.CommandLine"
#tool "nuget:?package=NUnit.Extension.NUnitV2Driver"
#tool "nuget:?package=NUnit.Extension.VSProjectLoader"
#tool "nuget:?package=NUnit.ConsoleRunner"
#tool "nuget:?package=NUnit.Extension.TeamCityEventListener"
#tool "nuget:?package=OctopusTools"
#tool "nuget:?package=JetBrains.dotCover.CommandLineTools"


//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var configurationNet40 = Argument("configuration", "Release");
var configurationNet45 = Argument("configuration", "ReleaseNet45");


///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////
var isCIBuild			= !BuildSystem.IsLocalBuild;
var solutionPath        = "./Checkout.ApiClient.sln";
var testPath            = "./Checkout.ApiClient.Tests/bin/" + configurationNet40 + "/Tests.dll";
var buildArtifacts      = Directory("./artifacts");
var libs                = Directory("./packages/_lib");

var gitVersionInfo = GitVersion(new GitVersionSettings {
	OutputType = GitVersionOutput.Json
});

var nugetVersion = gitVersionInfo.NuGetVersion;


///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////
Setup(context =>
{
	Information("Building Checkout NET Library v{0} with configurations {1} and {2}", nugetVersion, configurationNet40, configurationNet45);
	context.Tools.RegisterFile("./tools/nuget.exe");
});

Teardown(context =>
{
	Information("Finished running tasks. Test Path: {0}, configs: {1} and ", testPath, configurationNet40, configurationNet45);
});

//////////////////////////////////////////////////////////////////////
//  PRIVATE TASKS
//////////////////////////////////////////////////////////////////////

Task("__Clean")
	.Does(() =>
	{
		CleanDirectories(new DirectoryPath[] { buildArtifacts, libs });

		DotNetBuild(solutionPath, settings => settings
			.SetConfiguration(configurationNet40)
			.WithTarget("Clean")
			.SetVerbosity(Verbosity.Minimal));

		DotNetBuild(solutionPath, settings => settings
			.SetConfiguration(configurationNet45)
			.WithTarget("Clean")
			.SetVerbosity(Verbosity.Minimal));
	});

Task("__Restore")
	.Does(() =>
	{
		NuGetRestore(solutionPath);
	});

Task("__UpdateAssemblyVersionInformation")
	.WithCriteria(isCIBuild)
	.Does(() =>
	{
		GitVersion(new GitVersionSettings {
			UpdateAssemblyInfo = true, // Don't update all assembly files
			UpdateAssemblyInfoFilePath = "./src/shared/ApiClientAssemblyInfo.cs",
			OutputType = GitVersionOutput.BuildServer
		});

		Information("AssemblyVersion -> {0}", gitVersionInfo.AssemblySemVer);
		Information("AssemblyFileVersion -> {0}.0", gitVersionInfo.MajorMinorPatch);
		Information("AssemblyInformationalVersion -> {0}", gitVersionInfo.InformationalVersion);
	});

Task("__Build")
	.Does(() =>
	{
		var packagePath = string.Concat("\"", MakeAbsolute(buildArtifacts).FullPath, "\"");

		DotNetBuild(solutionPath, settings => settings
			.SetConfiguration(configurationNet40)
			.WithTarget("Rebuild")
			.SetVerbosity(Verbosity.Minimal)
			.WithProperty("WarningLevel", "0")

			// There's no point running build twice so create the in the initial build
			.WithProperty("RunOctoPack", "true")
			.WithProperty("OctoPackPackageVersion", nugetVersion)
			.WithProperty("OctoPackPublishPackageToFileShare", packagePath)
			.WithProperty("WarningLevel", "0"));		

		DotNetBuild(solutionPath, settings => settings
			.SetConfiguration(configurationNet45)
			.WithTarget("Rebuild")
			.SetVerbosity(Verbosity.Minimal)
			.WithProperty("WarningLevel", "0")

			// There's no point running build twice so create the in the initial build
			.WithProperty("RunOctoPack", "true")
			.WithProperty("OctoPackPackageVersion", nugetVersion)
			.WithProperty("OctoPackPublishPackageToFileShare", packagePath)
			.WithProperty("WarningLevel", "0"));		
	});

Task("__CreateNuGetPackage")
	.Does(() => 
	{
		FilePath nugetPath = Context.Tools.Resolve("nuget.exe");
		StartProcess(nugetPath, new ProcessSettings {
			Arguments = new ProcessArgumentBuilder().Append("pack").Append("./Checkout.ApiClient/NuGet.nuspec").Append("-Version " + gitVersionInfo.MajorMinorPatch)
		});
	});

Task("__Test")
	.Does(() =>
	{
		NUnit3(testPath, new NUnit3Settings {
			Configuration = configurationNet40,
			NoHeader = true,
			Results = buildArtifacts + File("TestResults.xml"),
			OutputFile = buildArtifacts + File("TestOutput.txt"), // Have to do that to redirect Console logging
			TeamCity = isCIBuild
		});
	});



Task("__OctoPush")
	.WithCriteria(isCIBuild)
	.Does(() =>
	{
		var packages = GetFiles("./artifacts/*.nupkg");

		OctoPush(
			EnvironmentVariable("Octopus_Server"),
			EnvironmentVariable("Octopus_ApiKey"),
			packages,
			new OctopusPushSettings {
				ReplaceExisting = true
			}
		);
	});


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Package")
	.IsDependentOn("__Clean")
	.IsDependentOn("__Restore")
	.IsDependentOn("__UpdateAssemblyVersionInformation")
	.IsDependentOn("__Build")
	.IsDependentOn("__CreateNuGetPackage")
    .IsDependentOn("__Test");


Task("Deploy")
	.IsDependentOn("Package")
	.IsDependentOn("__OctoPush");

Task("Default")
	.IsDependentOn("Package");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////
RunTarget(target);