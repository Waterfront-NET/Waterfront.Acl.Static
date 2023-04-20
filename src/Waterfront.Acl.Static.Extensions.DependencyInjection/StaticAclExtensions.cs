using Waterfront.Acl.Static.Authentication;
using Waterfront.Acl.Static.Authorization;
using Waterfront.Acl.Static.Configuration;
using Waterfront.Acl.Static.Models;
using Waterfront.Extensions.DependencyInjection;

namespace Waterfront.Acl.Static.Extensions.DependencyInjection;

public static class StaticAclExtensions
{
    public static IWaterfrontBuilder AddStaticAuthentication(
        this IWaterfrontBuilder builder,
        Action<StaticAclOptions> configureOptions
    )
    {
        return builder.WithAuthentication<StaticAclAuthenticationService, StaticAclOptions>(
            configureOptions
        );
    }

    public static IWaterfrontBuilder AddStaticAuthentication(
        this IWaterfrontBuilder builder,
        params StaticAclUser[] users
    )
    {
        return builder.AddStaticAuthentication(opt => opt.Users.AddRange(users));
    }

    public static IWaterfrontBuilder AddStaticAuthorization(
        this IWaterfrontBuilder builder,
        Action<StaticAclOptions> configureOptions
    )
    {
        return builder.WithAuthorization<StaticAclAuthorizationService, StaticAclOptions>(
            configureOptions
        );
    }

    public static IWaterfrontBuilder AddStaticAuthorization(
        this IWaterfrontBuilder builder,
        params StaticAclPolicy[] acl
    )
    {
        return builder.AddStaticAuthorization(opt => opt.Acl.AddRange(acl));
    }
}
