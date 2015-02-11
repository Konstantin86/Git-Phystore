using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Phystore.DAL.Entities;
using Phystore.WebApi.Controllers.Base;
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

    [Authorize]
    [Route("user")]
    public async Task<IHttpActionResult> GetCurrentUser()
    {
      var user = await AppUserManager.FindByNameAsync(User.Identity.Name);

      if (user != null)
      {
        return Ok(TheModelFactory.Create(user));
      }

      return NotFound();
    }

    [Route("user/{username}")]
    public async Task<IHttpActionResult> GetUserByName(string username)
    {
      var user = await AppUserManager.FindByNameAsync(username);

      if (user != null)
      {
        return Ok(TheModelFactory.Create(user));
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

      IdentityResult addUserToRoleResult = await AppUserManager.AddToRoleAsync(user.Id, requestModel.RoleName);

      if (!addUserToRoleResult.Succeeded) return GetErrorResult(addUserToRoleResult);

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

      return result.Succeeded 
        ? Redirect(new Uri(ConfigurationManager.AppSettings["webClientHostBaseUri"] + @"#/confirm"))
        : GetErrorResult(result);
    }

    [Authorize]
    [Route("ChangePassword")]
    public async Task<IHttpActionResult> ChangePassword(ChangePasswordRequestModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      return Ok();
    }

    [Authorize]
    [Route("update")]
    public async Task<IHttpActionResult> Update(UserUpdateRequestModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var currentUser = await AppUserManager.FindByNameAsync(User.Identity.Name);

      currentUser.FirstName = model.FirstName;
      currentUser.LastName = model.LastName;
      currentUser.Sex = model.Sex;
      currentUser.BirthDate = model.BirthDate;
      currentUser.Country = model.Country;
      currentUser.City = model.City;

      IdentityResult result = await AppUserManager.UpdateAsync(currentUser);

      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      return Ok();
    }

    // To test this method we need to issue HTTP DELETE request to the end point “api/accounts/user/{id}”.
    [Authorize]
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