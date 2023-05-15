using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Waterfront.Acl.Static.Authentication;
using Waterfront.Acl.Static.Authorization;
using Waterfront.Acl.Static.Configuration;
using Waterfront.Acl.Static.Models;
using Waterfront.Common.Authentication;
using Waterfront.Extensions.DependencyInjection;

namespace Waterfront.Acl.Static.Extensions.DependencyInjection;

public static partial class StaticAclExtensions
{
    public static IWaterfrontBuilder AddStaticAuthentication(
        this IWaterfrontBuilder builder,
        string name,
        Action<StaticAclOptions> configureOptions
    )
    {
        builder.Services.AddScoped<IAclAuthenticationService, StaticAclAuthenticationService>(
            services =>  new StaticAclAuthenticationService(
                services.GetRequiredService<ILoggerFactory>(),
                services.GetRequiredService<IOptionsSnapshot<StaticAclOptions>>().Get(name)
            )
        );
        builder.Services.AddTransient<IConfigureOptions<StaticAclOptions>>(
            _ => new ConfigureNamedOptions<StaticAclOptions>(name, configureOptions)
        );

        return builder;
    }

    public static IWaterfrontBuilder AddStaticAuthentication(
        this IWaterfrontBuilder builder,
        string name,
        params StaticAclUser[] users
    )
    {
        return builder.AddStaticAuthentication(
            name,
            options => {
                options.Users.Clear();
                options.Users.AddRange(users);
            }
        );
    }

    public static IWaterfrontBuilder AddStaticAuthentication(
        this IWaterfrontBuilder builder,
        Action<StaticAclOptions> configureOptions
    )
    {
        return builder.AddStaticAuthentication(Options.DefaultName, configureOptions);
    }

    public static IWaterfrontBuilder AddStaticAuthentication(
        this IWaterfrontBuilder builder,
        params StaticAclUser[] users
    )
    {
        return builder.AddStaticAuthentication(Options.DefaultName, users);
    }

    /*public static IWaterfrontBuilder AddStaticAuthentication(
        this IWaterfrontBuilder builder,
        Action<StaticAclOptions> configureOptions
    ) => builder.WithAuthentication<StaticAclAuthenticationService, StaticAclOptions>(
        configureOptions
    );

    public static IWaterfrontBuilder AddStaticAuthentication(
        this IWaterfrontBuilder builder,
        params StaticAclUser[] users
    ) => builder.AddStaticAuthentication(opt => opt.Users.AddRange(users));

    public static IWaterfrontBuilder AddStaticAuthorization(
        this IWaterfrontBuilder builder,
        Action<StaticAclOptions> configureOptions
    ) => builder.WithAuthorization<StaticAclAuthorizationService, StaticAclOptions>(
        configureOptions
    );

    public static IWaterfrontBuilder AddStaticAuthorization(
        this IWaterfrontBuilder builder,
        params StaticAclPolicy[] acl
    ) => builder.AddStaticAuthorization(opt => opt.Acl.AddRange(acl));*/
}
