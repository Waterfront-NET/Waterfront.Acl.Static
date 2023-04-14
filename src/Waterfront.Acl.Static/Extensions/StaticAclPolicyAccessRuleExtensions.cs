using System.Linq;
using Waterfront.Acl.Static.Models;
using Waterfront.Common.Acl;
using Waterfront.Core.Utility.Parsing.Acl;

namespace Waterfront.Acl.Static.Extensions;

public static class StaticAclPolicyAccessRuleExtensions
{
    public static AclAccessRule ToAclAccessRule(this StaticAclPolicyAccessRule self) =>
    new AclAccessRule {
        Type    = AclEntityParser.ParseResourceType(self.Type),
        Name    = self.Name,
        Actions = self.Actions.Select(AclEntityParser.ParseResourceAction)
    };

    public static AclPolicy ToAclPolicy(this StaticAclPolicy self) => new AclPolicy {
        Name   = self.Name,
        Access = self.Access.Select(ToAclAccessRule)
    };

    public static AclUser ToAclUser(this StaticAclUser self) => new AclUser {
        Username = self.Username,
        Acl      = self.Acl
    };
}
