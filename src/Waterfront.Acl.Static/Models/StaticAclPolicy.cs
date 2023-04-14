namespace Waterfront.Acl.Static.Models;

public class StaticAclPolicy
{
    public string Name { get; set; }
    public StaticAclPolicyAccessRule[] Access { get; set; }
    
    public override string ToString()
    {
        return $"StaticAclPolicy({Name})";
    }
}