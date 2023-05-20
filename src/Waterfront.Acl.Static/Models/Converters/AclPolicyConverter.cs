using Waterfront.Common.Acl;

namespace Waterfront.Acl.Static.Models.Converters;

public static class AclPolicyConverter
{
	public static AclPolicy ConvertToAclPolicy(StaticAclPolicy staticAclPolicy)
	{
		return new AclPolicy {
			Name = staticAclPolicy.Name,
			Access = staticAclPolicy.Access.Select(AclAccessRuleConverter.ConvertToAclAccessRule)
		};
	}

	public static StaticAclPolicy ConvertToStaticAclPolicy(AclPolicy aclPolicy)
	{
		return new StaticAclPolicy {
			Name = aclPolicy.Name,
			Access = aclPolicy.Access.Select(AclAccessRuleConverter.ConvertToStaticAclAccessRule).ToList()
		};
	}
}
