﻿using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using Owin;
using Phystore.DAL;
using Phystore.DAL.Managers;
using Phystore.WebApi.CompositionRoot;

namespace Phystore.WebApi
{
  public class Startup
  {
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