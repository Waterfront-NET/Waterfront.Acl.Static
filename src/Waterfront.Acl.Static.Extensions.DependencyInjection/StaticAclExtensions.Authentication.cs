using Waterfront.Acl.Static.Authentication;
using Waterfront.Acl.Static.Configuration;
using Waterfront.Acl.Static.Models;
using Waterfront.Extensions.DependencyInjection;

namespace Waterfront.Acl.Static.Extensions.DependencyInjection;

public static partial class StaticAclExtensions
{
	public static IWaterfrontBuilder WithStaticAuthentication(
		this IWaterfrontBuilder builder,
		string name,
		Action<StaticAclOptions> configureOptions
	)
	{
		return builder.WithAuthentication<StaticAclAuthenticationService, StaticAclOptions>(name, configureOptions);
	}

	public static IWaterfrontBuilder WithStaticAuthentication(
		this IWaterfrontBuilder builder,
		Action<StaticAclOptions> configureOptions
	)
	{
		return builder.WithAuthentication<StaticAclAuthenticationService, StaticAclOptions>(configureOptions);
	}

	public static IWaterfrontBuilder WithStaticAuthentication(
		this IWaterfrontBuilder builder,
		string name,
		params StaticAclUser[] users
	)
	{
		return builder.WithStaticAuthentication(name, options => options.Users.AddRange(users));
	}

	public static IWaterfrontBuilder WithStaticAuthentication(
		this IWaterfrontBuilder builder,
		params StaticAclUser[] users
	)
	{
		return builder.WithStaticAuthentication(options => options.Users.AddRange(users));
	}
}
