namespace Waterfront.Acl.Static.Models;

public class StaticAclPolicy
{
    public string Name { get; set; }
    public List<StaticAclPolicyAccessRule> Access { get; set; }

    public StaticAclPolicy()
    {
        Access = new List<StaticAclPolicyAccessRule>();
    }
    
    public override string ToString()
    {
        return $"StaticAclPolicy({Name})";
    }
}