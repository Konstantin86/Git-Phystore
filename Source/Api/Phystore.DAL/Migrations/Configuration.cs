using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Phystore.DAL.Entities;

namespace Phystore.DAL.Migrations
{
  using System;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Linq;

  internal sealed class Configuration : DbMigrationsConfiguration<Phystore.DAL.AppDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(Phystore.DAL.AppDbContext context)
    {
      //  This method will be called after migrating to the latest version.

      var manager = new UserManager<User>(new UserStore<User>(new AppDbContext()));

      var user = new User
      {
        UserName = "PowerUser",
        Email = "kostyan22@gmail.com",
        EmailConfirmed = true,
        FirstName = "Konstantin",
        LastName = "Lazurenko",
        JoinDate = DateTime.Now.AddDays(-1)
      };

      manager.Create(user, "mtecPass123");
    }
  }
}
