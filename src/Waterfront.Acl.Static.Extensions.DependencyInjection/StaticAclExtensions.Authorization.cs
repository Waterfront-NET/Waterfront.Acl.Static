using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Waterfront.Acl.Static.Authorization;
using Waterfront.Acl.Static.Configuration;
using Waterfront.Acl.Static.Models;
using Waterfront.Common.Authorization;
using Waterfront.Extensions.DependencyInjection;

namespace Waterfront.Acl.Static.Extensions.DependencyInjection;

public static partial class StaticAclExtensions
{
    public static IWaterfrontBuilder AddStaticAuthorization(
        this IWaterfrontBuilder builder,
        string name,
        Action<StaticAclOptions> configureOptions
    )
    {
        builder.Services.AddScoped<IAclAuthorizationService, StaticAclAuthorizationService>(
            services => new StaticAclAuthorizationService(
                services.GetRequiredService<ILoggerFactory>(),
                services.GetRequiredService<IOptionsSnapshot<StaticAclOptions>>().Get(name)
            )
        );
        builder.Services.AddTransient<IConfigureOptions<StaticAclOptions>>(
            _ => new ConfigureNamedOptions<StaticAclOptions>(name, configureOptions)
        );

        return builder;
    }

    public static IWaterfrontBuilder AddStaticAuthorization(
        this IWaterfrontBuilder builder,
        string name,
        params StaticAclPolicy[] acl
    )
    {
        return builder.AddStaticAuthorization(
            name,
            options => {
                options.Acl.Clear();
                options.Acl.AddRange(acl);
            }
        );
    }

    public static IWaterfrontBuilder AddStaticAuthorization(
        this IWaterfrontBuilder builder,
        Action<StaticAclOptions> configureOptions
    ) => builder.AddStaticAuthorization(Options.DefaultName, configureOptions);

    public static IWaterfrontBuilder AddStaticAuthorization(
        this IWaterfrontBuilder builder,
        params StaticAclPolicy[] acl
    ) => builder.AddStaticAuthorization(Options.DefaultName, acl);
}
