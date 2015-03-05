using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Phystore.DAL.Entities;
using Phystore.DAL.Services;

namespace Phystore.DAL.Managers
{
  public class AppUserManager : UserManager<User>
  {
    public AppUserManager(IUserStore<User> store)
      : base(store)
    {
    }

    public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
    {
      var appDbContext = context.Get<AppDbContext>();
      var appUserManager = new AppUserManager(new UserStore<User>(appDbContext)) { EmailService = new EmailService() };

      if (options.DataProtectionProvider != null)
      {
        appUserManager.UserTokenProvider = new DataProtectorTokenProvider<User>(options.DataProtectionProvider.Create("ASP.NET Identity"))
        {
          TokenLifespan = TimeSpan.FromHours(6)
        };
      }

      ConfigureUserPolicies(appUserManager);

      return appUserManager;
    }

    private static void ConfigureUserPolicies(AppUserManager appUserManager)
    {
      appUserManager.UserValidator = new UserValidator<User>(appUserManager)
      {
        AllowOnlyAlphanumericUserNames = true,
        RequireUniqueEmail = true
      };

      appUserManager.PasswordValidator = new PasswordValidator
      {
        RequiredLength = 6,
        RequireDigit = false,
        RequireLowercase = true,
        RequireUppercase = true,
      };
    }
  }
}