﻿using Microsoft.AspNet.Identity.EntityFramework;
using Phystore.DAL.Entities;

namespace Phystore.DAL
{
  public class AppDbContext : IdentityDbContext<User>
  {
    public AppDbContext() : base("DefaultConnection", throwIfV1Schema: false)
    {
      Configuration.ProxyCreationEnabled = false;
      Configuration.LazyLoadingEnabled = false;
    }

    public static AppDbContext Create()
    {
      return new AppDbContext();
    }
  }
}