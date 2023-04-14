using Waterfront.Acl.Static.Models;

namespace Waterfront.Acl.Static.Configuration;

public class StaticAclAuthenticationOptions
{
    public StaticAclUser[] Users { get; set; }
}