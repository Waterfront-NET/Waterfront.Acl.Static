using Waterfront.Common.Acl;

namespace Waterfront.Acl.Static.Models.Converters;

public static class AclUserConverter
{
	public static AclUser ConvertToAclUser(StaticAclUser staticAclUser)
	{
		return new AclUser {
			Username = staticAclUser.Username,
			Acl = staticAclUser.Acl.ToList()
		};
	}

	public static StaticAclUser ConvertToStaticAclUser(AclUser aclUser)
	{
		return new StaticAclUser {
			Username = aclUser.Username,
			Acl = aclUser.Acl.ToList()
		};
	}
}
