using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Phystore.DAL.Entities;
using Phystore.WebApi.Controllers.Base;
using Phystore.WebApi.Models;
using Phystore.WebApi.Models.Request;

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

      string code = await this.AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);

      var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { userId = user.Id, code }));

      await this.AppUserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

      return Created(location, TheModelFactory.Create(user));
    }

    [HttpGet]
    [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
    public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
    {
      if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
      {
        ModelState.AddModelError("", "User Id and Code are required");
        return BadRequest(ModelState);
      }

      IdentityResult result = await this.AppUserManager.ConfirmEmailAsync(userId, code);

      return result.Succeeded ? Ok() : GetErrorResult(result);
    }

    [Route("ChangePassword")]
    public async Task<IHttpActionResult> ChangePassword(ChangePasswordRequestModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      // TODO: The extension method “GetUserId” will not work because you are calling it as anonymous user and the system doesn’t know your identity, so hold on the testing until we implement authentication part.
      IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      return Ok();
    }

    // To test this method we need to issue HTTP DELETE request to the end point “api/accounts/user/{id}”.
    [Route("user/{id:guid}")]
    public async Task<IHttpActionResult> DeleteUser(string id)
    {
      //Only SuperAdmin or Admin can delete users (Later when implement roles)

      var appUser = await this.AppUserManager.FindByIdAsync(id);

      if (appUser != null)
      {
        IdentityResult result = await this.AppUserManager.DeleteAsync(appUser);

        if (!result.Succeeded)
        {
          return GetErrorResult(result);
        }

        return Ok();

      }

      return NotFound();
    }
  }
}