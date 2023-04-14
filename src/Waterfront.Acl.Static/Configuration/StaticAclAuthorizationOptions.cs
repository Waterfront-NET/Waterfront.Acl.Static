using Waterfront.Acl.Static.Models;

namespace Waterfront.Acl.Static.Configuration;

public class StaticAclAuthorizationOptions
{
    public StaticAclPolicy[] Acl { get; set; }
}
