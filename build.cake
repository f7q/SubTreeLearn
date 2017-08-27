#addin nuget:?package=Cake.Paket
//#addin nuget:?package=Cake.Figlet
//#addin paket:?package=Cake.Figlet&group=build/setup
#tool nuget:?package=Paket
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./src/Example/bin") + Directory(configuration);
var reports = "./Reports";
var nuGet = "./NuGet";

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Setup(context => 
{
    //Information(Figlet("Cake.Paket.Example"));
	Information("Cake.Paket.Example");
});

Task("Clean")
    .Does(() =>
{
    Information("Clean");
    //CleanDirectories(new[] {buildDir, reports, nuGet});
});
//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Clean")
    .Does(() =>
{
    Information("Restore");
    if(IsRunningOnWindows())
    {
        Information("PaketRestore");
        PaketRestore(new PaketPackSettings{ToolPath="./.paket"});
		//PaketRestore();
		/*
		using(var process = StartAndReturnProcess("./.paket/paket", new ProcessSettings{ Arguments = "install" }))
        {
            process.WaitForExit();
		    // This should output 0 as valid arguments supplied
		    Information("Exit code: {0}", process.GetExitCode());
        }
		*/
    }
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////
RunTarget(target);