#load ../data/*.cake

var mainBuildTask = Task("build");

foreach(var project in projects) {
  var task = Task(project.TaskName("build")).Does(() => {
    DotNetBuild(project.Path.ToString(), new DotNetBuildSettings {
      NoRestore = true,
      NoDependencies = true,
      Configuration = args.Configuration
    });

    if(args.Configuration is "Release" && !args.NoCopyArtifacts && !project.IsTest) {
      var source = project.Directory.Combine("bin/Release/net6.0");
      var archiveName = $"{project.Name}.{version.SemVer}.zip";
      Zip(source, paths.Libraries.CombineWithFilePath(archiveName));
    }
  }).WithCriteria(!args.NoBuild)
  .IsDependentOn(project.TaskName("restore"));

  project.Dependencies.ForEach(dep => task.IsDependentOn(dep.TaskName("build")));

  mainBuildTask.IsDependentOn(task);
}
