using Waterfront.Common.Acl;
using Waterfront.Core.Parsing.Acl;
using Waterfront.Core.Serialization.Acl;

namespace Waterfront.Acl.Static.Models.Converters;

public static class AclAccessRuleConverter
{
    public static AclAccessRule ConvertToAclAccessRule(StaticAclAccessRule staticAclAccessRule)
    {
        return new AclAccessRule {
            Name    = staticAclAccessRule.Name,
            Type    = AclEntityParser.ParseResourceType(staticAclAccessRule.Type),
            Actions = staticAclAccessRule.Actions.Select(AclEntityParser.ParseResourceAction)
        };
    }

    public static StaticAclAccessRule ConvertToStaticAclAccessRule(AclAccessRule aclAccessRule)
    {
        return new StaticAclAccessRule {
            Name    = aclAccessRule.Name,
            Type    = aclAccessRule.Type.ToSerialized(),
            Actions = aclAccessRule.Actions.Select(x => x.ToSerialized()).ToList()
        };
    }
}