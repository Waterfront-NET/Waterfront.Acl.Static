using System.ComponentModel.DataAnnotations;
using Waterfront.Acl.Static.Models;

namespace Waterfront.Acl.Static.Configuration;

public class StaticAclOptions
{
    public List<StaticAclUser> Users { get; set; }
    public List<StaticAclPolicy> Acl { get; set; }

    public bool HasUsers => Users is {Count: not 0};
    public bool HasAcl => Acl is {Count: not 0};

    public StaticAclOptions()
    {
        Users = new List<StaticAclUser>();
        Acl = new List<StaticAclPolicy>();
    }

    public void Validate()
    {
        List<ValidationException> exceptions = new List<ValidationException>(2);

        if (Users.Any(user => string.IsNullOrEmpty(user.Username)))
        {
            exceptions.Add(new ValidationException("All users should have username defined"));
        }

        if (Acl.Any(acl => string.IsNullOrEmpty(acl.Name)))
        {
            exceptions.Add(new ValidationException("All Acl policies should have name defined"));
        }

        if ( exceptions.Any() )
        {
            throw new AggregateException(exceptions);
        }
    }
}
