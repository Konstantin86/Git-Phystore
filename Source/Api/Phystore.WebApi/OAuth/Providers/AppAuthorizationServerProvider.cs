using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Phystore.DAL.Managers;

namespace Phystore.WebApi.OAuth.Providers
{
  public class AppAuthorizationServerProvider : OAuthAuthorizationServerProvider
  {
    public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    {
      context.Validated();
    }

    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    {
      var userManager = context.OwinContext.GetUserManager<AppUserManager>();
      context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

      // TODO FindByEmailAsync use for logging by e-mail

      IdentityUser user = await userManager.FindAsync(context.UserName, context.Password);

      if (user == null)
      {
        // Try to find by e-mail:
        user = await userManager.FindByEmailAsync(context.UserName);

                if (user != null)
                {
                    user = await userManager.FindAsync(user.UserName, context.Password);

                    if (user == null)
                    {
                        context.SetError("invalid_grant", "The user name or password is incorrect.");
                        return;
                    }
                }
                else
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
      }

      bool isEmailConfirmed = await userManager.IsEmailConfirmedAsync(user.Id);

      if (!isEmailConfirmed)
      {
        context.SetError("invalid_grant", "Email account is not confirmed.");
        return;
      }

      var identity = new ClaimsIdentity(context.Options.AuthenticationType);
      identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id));
      identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
      IList<string> userRoles = await userManager.GetRolesAsync(user.Id);

      foreach (string role in userRoles)
      {
        identity.AddClaim(new Claim(ClaimTypes.Role, role));
      }

      context.Validated(identity);
    }
  }
}