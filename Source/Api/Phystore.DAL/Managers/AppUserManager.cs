using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Phystore.DAL.Entities;

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
      var appUserManager = new AppUserManager(new UserStore<User>(appDbContext));
      return appUserManager;
    }
  }
}