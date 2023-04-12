// ReSharper disable UnusedAutoPropertyAccessor.Global

using Waterfront.Acl.Static.Models;

namespace Waterfront.Acl.Static.Configuration;

#pragma warning disable CS8618

public class StaticAclOptions
{
    public StaticAclUser[] Users { get; set; }
    public StaticAclPolicy[] Acl { get; set; }
}