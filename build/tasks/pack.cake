#load ../data/*.cake

var mainPackTask = Task("pack");

foreach(var project in projects.Where(p => !p.IsTest)) {
  var task = Task(project.TaskName("pack"))
  .Does(() => {
    DotNetPack(project.Path.ToString(), new DotNetPackSettings {
      Configuration = args.Configuration,
      NoBuild = true
    });

    if(args.Configuration is "Release" && !args.NoCopyArtifacts) {
      var packages = GetFiles(project.Directory.Combine($"bin/Release/{project.Name}.{version.SemVer}.{{nupkg,snupkg}}").ToString());
      packages.ToList().ForEach(package => CopyFileToDirectory(package, paths.Packages));
    }
  }).IsDependentOn(project.TaskName("build"));

  mainPackTask.IsDependentOn(task);
}
