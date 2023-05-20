using DotNet.Globbing;

namespace Waterfront.Acl.Static.Extensions;

public static class StringExtensions
{
	public static Glob ToGlob(this string self)
	{
		return self.ToGlob(new GlobOptions {Evaluation = new EvaluationOptions {CaseInsensitive = true}});
	}

	public static Glob ToGlob(this string self, GlobOptions options)
	{
		return Glob.Parse(self, options);
	}
}
