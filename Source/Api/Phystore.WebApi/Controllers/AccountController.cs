using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json.Linq;
using Phystore.DAL.Entities;
using Phystore.WebApi.Controllers.Base;
using Phystore.WebApi.Models;
using Phystore.WebApi.Models.Logins;
using Phystore.WebApi.Models.Request;
using Phystore.WebApi.OAuth.Results;

namespace Phystore.WebApi.Controllers
{
  [RoutePrefix("api/account")]
  public class AccountController : BaseApiController
  {
    private IAuthenticationManager Authentication
    {
      get { return Request.GetOwinContext().Authentication; }
    }

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

      var user = await AppUserManager.FindByNameAsync(User.Identity.Name);

      if (user == null)
      {
        return BadRequest("Auth db corrupted");
      }

      IdentityResult result = await AppUserManager.ChangePasswordAsync(user.Id, model.OldPassword, model.Password);

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
    [Route("user")]
    public async Task<IHttpActionResult> DeleteUser()
    {
      //Only SuperAdmin or Admin can delete users (Later when implement roles)
      //var user = string.IsNullOrEmpty(id)
      //  ? await AppUserManager.FindByNameAsync(User.Identity.Name)
      //  : await this.AppUserManager.FindByIdAsync(id);
      var user = await AppUserManager.FindByNameAsync(User.Identity.Name);

      if (user != null)
      {
        IdentityResult result = await AppUserManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
          return GetErrorResult(result);
        }

        return Ok();

      }

      return NotFound();
    }

    // GET api/account/externalLogin
    [OverrideAuthentication]
    [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
    [AllowAnonymous]
    [Route("externalLogin", Name = "externalLogin")]
    public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
    {
      string redirectUri = string.Empty;

      if (error != null)
      {
        return BadRequest(Uri.EscapeDataString(error));
      }

      if (!User.Identity.IsAuthenticated)
      {
        return new ChallengeResult(provider, this);
      }

      var redirectUriValidationResult = ValidateClientAndRedirectUri(Request, ref redirectUri);

      if (!string.IsNullOrWhiteSpace(redirectUriValidationResult))
      {
        return BadRequest(redirectUriValidationResult);
      }

      ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

      if (externalLogin == null)
      {
        return InternalServerError();
      }

      if (externalLogin.LoginProvider != provider)
      {
        Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        return new ChallengeResult(provider, this);
      }

      IdentityUser user = await AppUserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey));

      bool registered = user != null;

      redirectUri = string.Format("{0}#external_access_token={1}&provider={2}&haslocalaccount={3}&external_user_name={4}&email={5}",
                                      redirectUri,
                                      externalLogin.ExternalAccessToken,
                                      externalLogin.LoginProvider,
                                      registered.ToString(),
                                      externalLogin.UserName,
                                      externalLogin.Email);

      return Redirect(redirectUri);
    }

    // POST api/Account/RegisterExternal
    [AllowAnonymous]
    [Route("RegisterExternal")]
    public async Task<IHttpActionResult> RegisterExternal(RegisterExternalModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var verifiedAccessToken = await VerifyExternalAccessToken(model.Provider, model.ExternalAccessToken);
      if (verifiedAccessToken == null)
      {
        return BadRequest("Invalid Provider or External Access Token");
      }

      User user = await AppUserManager.FindAsync(new UserLoginInfo(model.Provider, verifiedAccessToken.user_id));

      bool hasRegistered = user != null;

      if (hasRegistered)
      {
        return BadRequest("External user is already registered");
      }

      user = new User { UserName = model.UserName, Email = model.Email, EmailConfirmed = true, JoinDate = DateTime.Now };

      IdentityResult result = await AppUserManager.CreateAsync(user);
      if (!result.Succeeded) return GetErrorResult(result);

      IdentityResult addUserToRoleResult = await AppUserManager.AddToRoleAsync(user.Id, "user");

      if (!addUserToRoleResult.Succeeded) return GetErrorResult(addUserToRoleResult);

      var info = new ExternalLoginInfo
      {
        DefaultUserName = model.UserName,
        Login = new UserLoginInfo(model.Provider, verifiedAccessToken.user_id)
      };

      result = await AppUserManager.AddLoginAsync(user.Id, info.Login);
      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      //generate access token response
      var accessTokenResponse = GenerateLocalAccessTokenResponse(model.UserName);

      return Ok(accessTokenResponse);
    }

    private string ValidateClientAndRedirectUri(HttpRequestMessage request, ref string redirectUriOutput)
    {

      Uri redirectUri;

      var redirectUriString = GetQueryString(Request, "redirect_uri");

      if (string.IsNullOrWhiteSpace(redirectUriString))
      {
        return "redirect_uri is required";
      }

      bool validUri = Uri.TryCreate(redirectUriString, UriKind.Absolute, out redirectUri);

      if (!validUri)
      {
        return "redirect_uri is invalid";
      }

      redirectUriOutput = redirectUri.AbsoluteUri;

      return string.Empty;
    }

    private string GetQueryString(HttpRequestMessage request, string key)
    {
      var queryStrings = request.GetQueryNameValuePairs();

      if (queryStrings == null) return null;

      var match = queryStrings.FirstOrDefault(keyValue => String.Compare(keyValue.Key, key, StringComparison.OrdinalIgnoreCase) == 0);

      if (string.IsNullOrEmpty(match.Value)) return null;

      return match.Value;
    }

    private async Task<ParsedExternalAccessToken> VerifyExternalAccessToken(string provider, string accessToken)
    {
      ParsedExternalAccessToken parsedToken = null;

      var verifyTokenEndPoint = "";

      if (provider == "Facebook")
      {
        //You can get it from here: https://developers.facebook.com/tools/accesstoken/
        //More about debug_tokn here: http://stackoverflow.com/questions/16641083/how-does-one-get-the-app-access-token-for-debug-token-inspection-on-facebook

        var appToken = "1540544966219959|eATZ1vQ6TzVBWDE5SS4MGo_5ymw";
        verifyTokenEndPoint = string.Format("https://graph.facebook.com/debug_token?input_token={0}&access_token={1}", accessToken, appToken);
      }
      else if (provider == "Google")
      {
        verifyTokenEndPoint = string.Format("https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={0}", accessToken);
      }
      else
      {
        return null;
      }

      var client = new HttpClient();
      var uri = new Uri(verifyTokenEndPoint);
      var response = await client.GetAsync(uri);

      if (response.IsSuccessStatusCode)
      {
        var content = await response.Content.ReadAsStringAsync();

        dynamic jObj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

        parsedToken = new ParsedExternalAccessToken();

        if (provider == "Facebook")
        {
          parsedToken.user_id = jObj["data"]["user_id"];
          parsedToken.app_id = jObj["data"]["app_id"];

          if (!string.Equals(Startup.FacebookAuthOptions.AppId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase))
          {
            return null;
          }
        }
        else if (provider == "Google")
        {
          parsedToken.user_id = jObj["user_id"];
          parsedToken.app_id = jObj["audience"];

          if (!string.Equals(Startup.GoogleAuthOptions.ClientId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase))
          {
            return null;
          }

        }

      }

      return parsedToken;
    }

    private JObject GenerateLocalAccessTokenResponse(string userName)
    {
      var tokenExpiration = TimeSpan.FromDays(1);

      ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

      identity.AddClaim(new Claim(ClaimTypes.Name, userName));
      identity.AddClaim(new Claim(ClaimTypes.Role, "user"));

      var props = new AuthenticationProperties()
      {
        IssuedUtc = DateTime.UtcNow,
        ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
      };

      var ticket = new AuthenticationTicket(identity, props);

      var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

      JObject tokenResponse = new JObject(
                                  new JProperty("userName", userName),
                                  new JProperty("access_token", accessToken),
                                  new JProperty("token_type", "bearer"),
                                  new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                  new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                                  new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString()));

      return tokenResponse;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("ObtainLocalAccessToken")]
    public async Task<IHttpActionResult> ObtainLocalAccessToken(string provider, string externalAccessToken)
    {
      if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(externalAccessToken))
      {
        return BadRequest("Provider or external access token is not sent");
      }

      var verifiedAccessToken = await VerifyExternalAccessToken(provider, externalAccessToken);
      if (verifiedAccessToken == null)
      {
        return BadRequest("Invalid Provider or External Access Token");
      }

      IdentityUser user = await AppUserManager.FindAsync(new UserLoginInfo(provider, verifiedAccessToken.user_id));

      bool hasRegistered = user != null;

      if (!hasRegistered)
      {
        return BadRequest("External user is not registered");
      }

      //generate access token response
      var accessTokenResponse = GenerateLocalAccessTokenResponse(user.UserName);

      return Ok(accessTokenResponse);
    }

    [Authorize]
    [Route("photo", Name = "photo")]
    [HttpPost]
    [ResponseType(typeof(JObject))]
    public async Task<IHttpActionResult> PostPhoto()
    {
      if (!Request.Content.IsMimeMultipartContent())
      {
        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
      }

      var user = await AppUserManager.FindByNameAsync(User.Identity.Name);

      string root = HttpContext.Current.Server.MapPath("~/App_Data");

      DirectoryInfo di = new DirectoryInfo(root);
      foreach (FileInfo fi in di.GetFiles())
      {
        fi.Delete();
      }

      var provider = new MultipartFormDataStreamProvider(root);

      try
      {
        await Request.Content.ReadAsMultipartAsync(provider);

        foreach (MultipartFileData file in provider.FileData)
        {
          string photofileName = Guid.NewGuid().ToString();
          
          CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
          CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
          CloudBlobContainer container = blobClient.GetContainerReference("media");

          string[] fileNameParts = file.Headers.ContentDisposition.FileName.Split('.');

          if (!fileNameParts.Any())
          {
            throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
          }

          string ext = file.Headers.ContentDisposition.FileName.Split('.').Last();
          var bloblPhotoName = photofileName + "." + ext.Substring(0, ext.Length - 1);
          CloudBlockBlob blockBlob = container.GetBlockBlobReference(bloblPhotoName);
          blockBlob.Properties.ContentType = file.Headers.ContentType.MediaType;
          //blockBlob.SetProperties();

          using (var fileStream = File.OpenRead(file.LocalFileName))
          {
            blockBlob.UploadFromStream(fileStream);
          }

          user.PhotoPath = bloblPhotoName;

          IdentityResult result = await AppUserManager.UpdateAsync(user);

          if (!result.Succeeded)
          {
            return GetErrorResult(result);
          }

          return Ok();
        }
      }
      catch (Exception)
      {
        throw;
      }

      return Ok();
    }
  }
}