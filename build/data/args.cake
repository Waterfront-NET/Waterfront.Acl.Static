
static BuildArguments args;
args = new BuildArguments(Context);

class BuildArguments {
  private readonly ICakeContext _context;

  public string Configuration => _context.Argument("configuration", _context.Argument("c", "Debug"));
  public string Target => _context.Argument("target", _context.Argument("t", "build"));
  public bool NoBuild => _context.HasArgument("no-build");
  public bool NoCopyArtifacts => _context.HasArgument("no-copy-artifacts");

  public BuildArguments(ICakeContext context) {
    _context = context;
  }
}
