using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Keepfit.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Keepfit.DAL.Migrations
{
  internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(AppDbContext context)
    {
      //if (System.Diagnostics.Debugger.IsAttached == false)
      //  System.Diagnostics.Debugger.Launch();
      //  This method will be called after migrating to the latest version.

      var appDbContext = new AppDbContext();
      var userManager = new UserManager<AppUser>(new UserStore<AppUser>(appDbContext));
      var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(appDbContext));

      List<AppUser> existingUsers = userManager.Users.ToList();

      foreach (var usr in existingUsers)
      {
        var roles = userManager.GetRoles(usr.Id);
        foreach (var roleName in roles)
        {
          userManager.RemoveFromRole(usr.Id, roleName);
        }
      }

      List<IdentityRole> existingRoles = roleManager.Roles.ToList();
      foreach (var identityRole in existingRoles)
      {
        roleManager.Delete(identityRole);
      }

      foreach (var usr in existingUsers)
      {
        userManager.Delete(usr);
      }

      IdentityRole userRole = new IdentityRole("user");
      IdentityRole adminRole = new IdentityRole("admin");

      roleManager.Create(userRole);
      roleManager.Create(adminRole);

      var user = new AppUser
      {
        UserName = "PowerUser",
        Email = "test@gmail.com",
        EmailConfirmed = true,
        FirstName = "Konstantin",
        LastName = "Lazurenko",
        JoinDate = DateTime.Now.AddDays(-1)
      };

      //TODO create rolemanager and add two roles: user and admin

      IdentityResult ir = userManager.Create(user, "mtecPass123");
      if (ir.Succeeded)
      {
        userManager.AddToRoles(user.Id, "user", "admin");
      }
      else
      {
        Console.WriteLine(string.Join(Environment.NewLine, ir.Errors));
      }

      context.Exercises.Add(new Exercise
      {
        Name = "Test Exercise",
        Description = "Test Description",
        Category = "Loosing weight"
      });
    }
  }
}
