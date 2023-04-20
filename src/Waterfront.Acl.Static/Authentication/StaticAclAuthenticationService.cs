using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Waterfront.Acl.Static.Configuration;
using Waterfront.Acl.Static.Extensions;
using Waterfront.Acl.Static.Models;
using Waterfront.Common.Authentication;
using Waterfront.Common.Tokens.Requests;
using Waterfront.Core.Authentication;

namespace Waterfront.Acl.Static.Authentication;

public class StaticAclAuthenticationService : AclAuthenticationServiceBase<StaticAclOptions>
{
    public StaticAclAuthenticationService(ILoggerFactory loggerFactory, IOptions<StaticAclOptions> options) : base(
        loggerFactory,
        options
    ) {}

    public override ValueTask<AclAuthenticationResult> AuthenticateAsync(TokenRequest request)
    {
        Logger.LogDebug("Authorizing token request: {RequestId}", request.Id);

        AclAuthenticationResult result = new AclAuthenticationResult {};

        if (TryAuthenticateWithBasicCredentials(request, out result))
        {
            Logger.LogDebug("Basic credentials matched");
        }
        else if (TryAuthenticateWithConnectionCredentials(request, out result))
        {
            Logger.LogDebug("Connection credentials matched");
        }
        else
        {
            result = TryAuthenticateWithFallbackPolicy();

            if (result.IsSuccessful)
            {
                Logger.LogDebug("Found anonymous user");
            }
            else
            {
                Logger.LogDebug("Auth failed");
            }
        }

        return ValueTask.FromResult(result);
    }

    private bool TryAuthenticateWithConnectionCredentials(TokenRequest request, out AclAuthenticationResult result)
    {
        string matchTarget = request.ConnectionCredentials.ToString();

        StaticAclUser? user = Options.Value.Users.FirstOrDefault(
            user => !string.IsNullOrEmpty(user.Ip) && user.Ip.ToGlob().IsMatch(matchTarget)
        );

        result = new AclAuthenticationResult {User = user?.ToAclUser()};
        return result.IsSuccessful;
    }

    private bool TryAuthenticateWithBasicCredentials(TokenRequest request, out AclAuthenticationResult result)
    {
        if (!request.BasicCredentials.HasValue)
        {
            result = new AclAuthenticationResult {};
            return false;
        }

        StaticAclUser? user = Options.Value.Users.FirstOrDefault(
            user => user.Username.Equals(request.BasicCredentials.Username)
        );

        if (user == null)
        {
            result = new AclAuthenticationResult {};

            return false;
        }

        if (!string.IsNullOrEmpty(user.Password))
        {
            // This nesting is on purpose
            if (BCrypt.Net.BCrypt.Verify(request.BasicCredentials.Password, user.Password))
            {
                result = new AclAuthenticationResult {User = user.ToAclUser()};
                return true;
            }
        }
        else if (!string.IsNullOrEmpty(user.PlainTextPassword) &&
                 user.PlainTextPassword.Equals(request.BasicCredentials.Password))
        {
            result = new AclAuthenticationResult {User = user.ToAclUser()};
            return true;
        }

        result = new AclAuthenticationResult {};

        return false;
    }

    AclAuthenticationResult TryAuthenticateWithFallbackPolicy()
    {
        StaticAclUser? anonUser = Options.Value.Users.FirstOrDefault(
            user => string.IsNullOrEmpty(user.Password) &&
                    string.IsNullOrEmpty(user.PlainTextPassword) &&
                    string.IsNullOrEmpty(user.Ip)
        );

        return new AclAuthenticationResult {User = anonUser?.ToAclUser()};
    }
}
