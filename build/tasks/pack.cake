#load ../data/*.cake

var mainPackTask = Task("pack");

foreach(var project in projects.Where(p => !p.IsTest)) {
  var task = Task(project.TaskName("pack"))
  .Does(() => {
    DotNetPack(project.Path.ToString(), new DotNetPackSettings {
      Configuration = args.Configuration,
      NoBuild = true
    });
  }).IsDependentOn(project.TaskName("build"));

  mainPackTask.IsDependentOn(task);
}
