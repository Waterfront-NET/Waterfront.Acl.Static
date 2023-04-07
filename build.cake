#tool "dotnet:?package=GitVersion.Tool&version=5.12.0"

var projects = new {
  aclStatic = new {
    path = new FilePath("src/Waterfront.Acl.Static/Waterfront.Acl.Static.csproj")
  },
  aclStaticModels = new {
    path = new FilePath("src/Waterfront.Acl.Static.Models/Waterfront.Acl.Static.Models.csproj")
  }
};

var target = Argument("target", "build");
var configuration = Argument("configuration", Argument("c", "Release"));

var versionInfo = GitVersion();

Task("patch-assembly-versions").Does(() => {
  XmlPoke(projects.aclStatic.path, "/Project[0]/PropertyGroup[0]/Version", versionInfo.InformationalVersion);
  XmlPoke(projects.aclStaticModels.path, "/Project[0]/PropertyGroup[0]/Version", versionInfo.InformationalVersion);
});

Task("build").Does(() => {
  Information("Building projects with version {0}", versionInfo.SemVer);

  var modelsProjectPath = "src/Waterfront.Acl.Static.Models/Waterfront.Acl.Static.Models.csproj";
  var mainProjectPath = "src/Waterfront.Acl.Static/Waterfront.Acl.Static.csproj";

  DotNetRestore(modelsProjectPath);
  DotNetRestore(mainProjectPath);

  DotNetBuild(modelsProjectPath, new DotNetBuildSettings {
    Configuration = configuration,
    NoRestore = true
  });

  DotNetBuild(mainProjectPath, new DotNetBuildSettings{
    Configuration = configuration,
    NoRestore = true,
    NoDependencies = true
  });
});




RunTarget(target);
