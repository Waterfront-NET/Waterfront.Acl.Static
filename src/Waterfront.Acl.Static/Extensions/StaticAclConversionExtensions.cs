using Waterfront.Acl.Static.Models;
using Waterfront.Acl.Static.Models.Converters;
using Waterfront.Common.Acl;
using Waterfront.Core.Parsing.Acl;

namespace Waterfront.Acl.Static.Extensions;

public static class StaticAclConversionExtensions
{
    public static AclAccessRule ToAclAccessRule(this StaticAclAccessRule self) =>
    AclAccessRuleConverter.ConvertToAclAccessRule(self);

    public static StaticAclAccessRule ToStaticAclAccessRule(this AclAccessRule self) =>
    AclAccessRuleConverter.ConvertToStaticAclAccessRule(self);

    public static AclPolicy ToAclPolicy(this StaticAclPolicy self) =>
    AclPolicyConverter.ConvertToAclPolicy(self);

    public static StaticAclPolicy ToStaticAclPolicy(this AclPolicy self) =>
    AclPolicyConverter.ConvertToStaticAclPolicy(self);

    public static AclUser ToAclUser(this StaticAclUser self) =>
    AclUserConverter.ConvertToAclUser(self);

    public static StaticAclUser ToStaticAclUser(this AclUser self) =>
    AclUserConverter.ConvertToStaticAclUser(self);
}
