using Waterfront.Acl.Static.Models;

namespace Waterfront.Acl.Static.Extensions;

public static class StaticAclUserExtensions
{
    public static bool IsAnonymous(this StaticAclUser self) =>
    string.IsNullOrEmpty(self.Password) &&
    string.IsNullOrEmpty(self.PlainTextPassword) &&
    string.IsNullOrEmpty(self.Ip);
}
