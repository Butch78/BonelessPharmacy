// Build a Production Version of the Application
Task("Production-Build")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() => 
{
    Information("Building Production Build...");
    var settings = new DotNetCoreBuildSettings
    {
        Framework = "netcoreapp2.0",
        Configuration = "Production",
        OutputDirectory = "./bin/Production"
    };
    DotNetCoreBuild("./BonelessPharmacyBackend.csproj", settings);
    Information("Finished Build :)");
}).OnError(ex => {
    Error("Build Failed :(");
    throw ex;
});

// Run the MS Test tests
Task("Run-Unit-Tests")
    .Does(() =>
{
    Information("Running unit tests...");
    DotNetCoreTest("./../backend-tests/backend-tests.csproj");
    Information("Finished Testing :)");
}).OnError(ex => {
    Error("Tests Failed :(");
    throw ex;
});

// Run Target
RunTarget("Production-Build");