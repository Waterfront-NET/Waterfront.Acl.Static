#load build/data/*.cake
#load build/tasks/*.cake

Setup(ctx => {
  EnsureDirectoryExists(paths.Packages);
  EnsureDirectoryExists(paths.Libraries);

  Environment.SetEnvironmentVariable("GitVersion_SemVer", version.SemVer);
  Environment.SetEnvironmentVariable("GitVersion_InformationalVersion", version.InformationalVersion);
});

RunTarget(args.Target);
