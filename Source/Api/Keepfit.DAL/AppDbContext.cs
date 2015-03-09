using System.Data.Entity;
using Keepfit.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Keepfit.DAL
{
  public class AppDbContext : IdentityDbContext<AppUser>
  {
    public DbSet<Exercise> Exercises { get; set; }

    public AppDbContext() : base("DefaultConnection", throwIfV1Schema: false)
    {
      //Configuration.ProxyCreationEnabled = false;
      //Configuration.LazyLoadingEnabled = false;
    }

    public static AppDbContext Create()
    {
      return new AppDbContext();
    }
  }
}