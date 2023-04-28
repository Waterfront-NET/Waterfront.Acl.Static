#addin nuget:?package=Cake.Incubator&version=8.0.0
#load args.cake
#load paths.cake

static List<BuildProject> projects;
projects = ParseSolution(paths.Solution).Projects.Where(project => project.Type == "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}")
.Select(project => {
  var fullProjectPath = project.Path.MakeAbsolute(paths.Solution.GetDirectory());
  var parsedProject = ParseProject(project.Path, args.Configuration);

  List<BuildProject> references = new(parsedProject.ProjectReferences.Select(reference => {
    var fullPath = FilePath.FromString(reference.RelativePath).MakeAbsolute(fullProjectPath.GetDirectory());
    return new BuildProject {
      Name = fullPath.GetFilenameWithoutExtension().ToString(),
      Path = fullPath
    };
  }));

  var buildProject = new BuildProject {
    Name = project.Name,
    Path = fullProjectPath,
    Dependencies = references
  };

  return buildProject;
}).ToList();

class BuildProject {
  public string Name { get; init; }
  public string ShortName => Name.Replace("Waterfront.", string.Empty).ToLowerInvariant();
  public FilePath Path { get; init; }
  public bool IsTest => Path.GetFilenameWithoutExtension().ToString().EndsWith(".Tests");
  public List<BuildProject> Dependencies { get; init; }

  public string TaskName(string task) => $":{ShortName}:{task}";
}
