using Waterfront.Acl.Static.Authorization;
using Waterfront.Acl.Static.Configuration;
using Waterfront.Acl.Static.Models;
using Waterfront.Extensions.DependencyInjection;

namespace Waterfront.Acl.Static.Extensions.DependencyInjection;

public static partial class StaticAclExtensions
{
	public static IWaterfrontBuilder WithStaticAuthorization(
		this IWaterfrontBuilder builder,
		string name,
		Action<StaticAclOptions> configureOptions
	)
	{
		return builder.WithAuthorization<StaticAclAuthorizationService, StaticAclOptions>(name, configureOptions);
	}

	public static IWaterfrontBuilder WithStaticAuthorization(
		this IWaterfrontBuilder builder,
		Action<StaticAclOptions> configureOptions
	)
	{
		return builder.WithAuthorization<StaticAclAuthorizationService, StaticAclOptions>(configureOptions);
	}

	public static IWaterfrontBuilder WithStaticAuthorization(
		this IWaterfrontBuilder builder,
		string name,
		params StaticAclPolicy[] policies
	)
	{
		return builder.WithStaticAuthorization(name, options => options.Acl.AddRange(policies));
	}

	public static IWaterfrontBuilder WithStaticAuthorization(
		this IWaterfrontBuilder builder,
		params StaticAclPolicy[] policies
	)
	{
		return builder.WithStaticAuthorization(options => options.Acl.AddRange(policies));
	}
}
