#load ../data/*.cake

var packages = GetFiles(paths.Packages.Combine("*.nupkg").ToString());

Task("push/packages/nuget")
.WithCriteria(packages.Any())
.IsDependentOn("push/packages/nuget/setup-source")
.DoesForEach(packages, package => {
  Information(
    "Pushing package {0} to nuget.org package source",
    package.GetFilenameWithoutExtension().ToString()
  );

  DotNetNuGetPush(package, new DotNetNuGetPushSettings {
    Source = "nuget.org",
    ApiKey = apikeys.Nuget
  });
});

Task("push/packages/myget");

Task("push/packages/github")
.WithCriteria(packages.Any())
.IsDependentOn("push/packages/github/setup-source")
.DoesForEach(packages, package => {
  Information(
    "Pushing package {0} to nuget.pkg.github.com package source",
    package.GetFilenameWithoutExtension().ToString()
  );

  DotNetNuGetPush(package, new DotNetNuGetPushSettings {
    Source = "nuget.pkg.github.com",
    ApiKey = apikeys.Github
  });
});

Task("push/release-assets/github");



Task("push/packages/nuget/setup-source")
.WithCriteria(!DotNetNuGetHasSource("nuget.org"), "nuget.org source already exists")
.Does(() => {
  Information("Setting up nuget.org source");
  DotNetNuGetAddSource("nuget.org", new DotNetNuGetSourceSettings {
    Source = "https://api.nuget.org/v3/index.json"
  });
});

Task("push/packages/github/setup-source")
.WithCriteria(!DotNetNuGetHasSource("nuget.pkg.github.com"), "nuget.pkg.github.com source already exists")
.Does(() => {
  Information("Setting up nuget.pkg.github.com source");
  DotNetNuGetAddSource("nuget.pkg.github.com", new DotNetNuGetSourceSettings {
    Source = "https://nuget.pkg.github.com/Waterfront-NET/index.json",
    UserName = "USERNAME",
    Password = apikeys.Github,
    StorePasswordInClearText = true
  });
});
