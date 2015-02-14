using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using Phystore.DAL;
using Phystore.DAL.Managers;
using Phystore.WebApi.CompositionRoot;
using Phystore.WebApi.OAuth.Providers;

namespace Phystore.WebApi
{
  public class Startup
  {
    public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
    public static GoogleOAuth2AuthenticationOptions GoogleAuthOptions { get; private set; }
    public static FacebookAuthenticationOptions FacebookAuthOptions { get; private set; }
 
    public void Configuration(IAppBuilder app)
    {
      var config = new HttpConfiguration();

      var diContainer = Bootstrapper.Instance.GetContainer(Assembly.GetExecutingAssembly());

      config.DependencyResolver = new AutofacWebApiDependencyResolver(diContainer);

      ConfigureAuth(app);

      ConfigureWebApi(config);

      app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

      app.UseAutofacMiddleware(diContainer);
      app.UseAutofacWebApi(config);

      app.UseWebApi(config);

    }

    private void ConfigureAuth(IAppBuilder app)
    {
      // Configure the db context and user manager to use a single instance per request
      app.CreatePerOwinContext(AppDbContext.Create);
      app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
      app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);

      OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
      {
        // Should be turned off in production:
        AllowInsecureHttp = true,
        TokenEndpointPath = new PathString("/token"),
        AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
        Provider = new AppAuthorizationServerProvider()
      };

      //use a cookie to temporarily store information about a user logging in with a third party login provider
      app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
      OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

      //Configure Google External Login
      GoogleAuthOptions = new GoogleOAuth2AuthenticationOptions()
      {
        ClientId = "762577690085-8q5b7s18bmdfjel6h8ihcc14tmongbd2.apps.googleusercontent.com",
        ClientSecret = "MuYGSdOcr5suTwYiUjOrh_52",
        Provider = new GoogleAuthProvider()
      };
      app.UseGoogleAuthentication(GoogleAuthOptions);

      //Configure Facebook External Login
      FacebookAuthOptions = new FacebookAuthenticationOptions
      {
        AppId = "1540544966219959",
        AppSecret = "afd3895f4a0dd8dc179f7d8a62ece758",
        Provider = new FacebookAuthProvider()
      };
      app.UseFacebookAuthentication(FacebookAuthOptions);

      // Token Generation
      app.UseOAuthAuthorizationServer(oAuthServerOptions);
      app.UseOAuthBearerAuthentication(OAuthBearerOptions);

      // Plugin the OAuth bearer tokens generation and Consumption will be here
    }

    private void ConfigureWebApi(HttpConfiguration config)
    {
      config.MapHttpAttributeRoutes();

      var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
      jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    }
  }
}