﻿using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using Keepfit.DAL;
using Keepfit.DAL.Managers;
using Keepfit.WebApi.CompositionRoot;
using Keepfit.WebApi.OAuth.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;

namespace Keepfit.WebApi
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

      ConfigureAuth(app);

      ConfigureWebApi(config);

      app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

      app.UseAutofacMiddleware(diContainer);
      app.UseAutofacWebApi(config);

      app.UseWebApi(config);

    }

    private void ConfigureAuth(IAppBuilder app)
    {
      app.CreatePerOwinContext(AppDbContext.Create);
      app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
      app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);

      OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
      {
        AllowInsecureHttp = true,
        TokenEndpointPath = new PathString("/token"),
        AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
        Provider = new AppAuthorizationServerProvider()
      };

      app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
      OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

      GoogleAuthOptions = new GoogleOAuth2AuthenticationOptions
      {
        ClientId = "762577690085-8q5b7s18bmdfjel6h8ihcc14tmongbd2.apps.googleusercontent.com",
        ClientSecret = "MuYGSdOcr5suTwYiUjOrh_52",
        Provider = new GoogleAuthProvider()
      };
      app.UseGoogleAuthentication(GoogleAuthOptions);

      FacebookAuthOptions = new FacebookAuthenticationOptions
      {
        AppId = "1540544966219959",
        AppSecret = "afd3895f4a0dd8dc179f7d8a62ece758",
        Provider = new FacebookAuthProvider()
      };
      app.UseFacebookAuthentication(FacebookAuthOptions);

      app.UseOAuthAuthorizationServer(oAuthServerOptions);
      app.UseOAuthBearerAuthentication(OAuthBearerOptions);
    }

    private void ConfigureWebApi(HttpConfiguration config)
    {
      config.MapHttpAttributeRoutes();
      var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
      jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    }
  }
}