namespace Waterfront.Acl.Static.Models;

public class StaticAclPolicyAccessRule
{
    public string Type { get; set; }
    public string Name { get; set; }
    public List<string> Actions { get; set; }

    public StaticAclPolicyAccessRule()
    {
        Actions = new List<string>();
    }

    public override string ToString()
    {
        return $"StaticAclPolicyAccessRule({Type}:{Name}:{string.Join(",", Actions)})";
    }
}