#load ../data/*.cake

var mainBuildTask = Task("build");

foreach(var project in projects) {
  var task = Task(project.TaskName("build")).Does(() => {
    DotNetBuild(project.Path.ToString(), new DotNetBuildSettings {
      NoRestore = true,
      NoDependencies = true,
      Configuration = args.Configuration
    });
  }).WithCriteria(!args.NoBuild)
  .IsDependentOn(project.TaskName("restore"));

  project.Dependencies.ForEach(dep => task.IsDependentOn(dep.TaskName("build")));

  mainBuildTask.IsDependentOn(task);
}
