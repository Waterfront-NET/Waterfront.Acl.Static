namespace Waterfront.Acl.Static.Models;

public class StaticAclPolicyAccessRule
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string[] Actions { get; set; }

    public override string ToString()
    {
        return $"StaticACLPolicyAccessRule({Type}:{Name}:{string.Join(",", Actions)})";
    }
}