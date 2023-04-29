#pragma warning disable CS8618

namespace Waterfront.Acl.Static.Models;

public class StaticAclAccessRule
{
  public string Type { get; set; }
  public string Name { get; set; }
  public List<string> Actions { get; set; }

  public StaticAclAccessRule()
  {
    Actions = new List<string>();
  }

  public override string ToString()
  {
    return $"StaticAclPolicyAccessRule({Type}:{Name}:{string.Join(",", Actions)})";
  }
}
