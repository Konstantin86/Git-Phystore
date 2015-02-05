using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Phystore.DAL.Entities;
using Phystore.WebApi.Controllers.Base;
using Phystore.WebApi.Models;

namespace Phystore.WebApi.Controllers
{
  [RoutePrefix("api/account")]
  public class AccountController : BaseApiController
  {

    [Route("users")]
    public IHttpActionResult GetUsers()
    {
      return Ok(AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
    }

    [Route("user/{id:guid}", Name = "GetUserById")]
    public async Task<IHttpActionResult> GetUser(string id)
    {
      var user = await AppUserManager.FindByIdAsync(id);

      if (user != null)
      {
        return Ok(TheModelFactory.Create(user));
      }

      return NotFound();
    }

    [Route("user/{username}")]
    public async Task<IHttpActionResult> GetUserByName(string username)
    {
      var user = await this.AppUserManager.FindByNameAsync(username);

      if (user != null)
      {
        return Ok(this.TheModelFactory.Create(user));
      }

      return NotFound();
    }

    [Route("create")]
    public async Task<IHttpActionResult> CreateUser(UserAddRequestModel requestModel)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);

      var user = new User
      {
        UserName = requestModel.Username,
        Email = requestModel.Email,
        FirstName = requestModel.FirstName,
        LastName = requestModel.LastName,
        JoinDate = DateTime.Now.Date,
      };

      IdentityResult addUserResult = await AppUserManager.CreateAsync(user, requestModel.Password);

      if (!addUserResult.Succeeded) return GetErrorResult(addUserResult);

      var location = new Uri(Url.Link("GetUserById", new { id = user.Id }));

      return Created(location, TheModelFactory.Create(user));
    }
  }
}