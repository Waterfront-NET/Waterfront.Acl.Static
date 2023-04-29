#pragma warning disable CS8618

namespace Waterfront.Acl.Static.Models;

public class StaticAclPolicy
{
  public string Name { get; set; }
  public List<StaticAclAccessRule> Access { get; set; }

  public StaticAclPolicy()
  {
    Access = new List<StaticAclAccessRule>();
  }

  public override string ToString()
  {
    return $"StaticAclPolicy({Name})";
  }
}
