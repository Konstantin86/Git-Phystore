using System.Net.Http;
using System.Web.Http.Routing;
using Phystore.DAL.Entities;
using Phystore.DAL.Managers;

namespace Phystore.WebApi.Models.Factory
{
  public class ModelFactory
  {
    private readonly UrlHelper _urlHelper;
    private readonly AppUserManager _appUserManager;

    public ModelFactory(HttpRequestMessage request, AppUserManager appUserManager)
    {
      _urlHelper = new UrlHelper(request);
      _appUserManager = appUserManager;
    }

    public UserModel Create(User user)
    {
      return new UserModel
      {
        Url = _urlHelper.Link("GetUserById", new { id = user.Id }),
        Id = user.Id,
        UserName = user.UserName,
        FirstName = user.FirstName,
        LastName = user.LastName,
        FullName = string.Format("{0} {1}", user.FirstName, user.LastName),
        Email = user.Email,
        EmailConfirmed = user.EmailConfirmed,
        Sex = user.Sex,
        Country = user.Country,
        City = user.City,
        BirthDate = user.BirthDate,
        JoinDate = user.JoinDate,
        Roles = _appUserManager.GetRolesAsync(user.Id).Result,
        Claims = _appUserManager.GetClaimsAsync(user.Id).Result
      };
    }
  }
}