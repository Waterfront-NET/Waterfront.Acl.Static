namespace Waterfront.Acl.Static.Models;

public class StaticAclUser
{
    public string Username { get; set; }
    public string? Ip { get; set; }
    public string? PlainTextPassword { get; set; }
    public string? Password { get; set; }
    public List<string> Acl { get; set; }

    public StaticAclUser()
    {
        Acl = new List<string>();
    }
    public override string ToString()
    {
        return $"StaticAclUser({Username})";
    }
}