#load build/data/*.cake
#load build/tasks/*.cake

Setup(ctx => {
  EnsureDirectoryExists(paths.Packages);
  EnsureDirectoryExists(paths.Libraries);

  Environment.SetEnvironmentVariable("SEMVER", version.SemVer);
  Environment.SetEnvironmentVariable("INFO_VER", version.InformationalVersion);
});

RunTarget(args.Target);
