using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json.Linq;

namespace Phystore.WebApi.Controllers
{
  [RoutePrefix("api/image")]
  public class ImageController : ApiController
  {
    [Route("test", Name = "test")]
    [HttpPost]
    [ResponseType(typeof(JObject))]
    public async Task<IHttpActionResult> Postimage()
    {
      // Check if the request contains multipart/form-data.
      if (!Request.Content.IsMimeMultipartContent())
      {
        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
      }

      string root = HttpContext.Current.Server.MapPath("~/App_Data");
      var provider = new MultipartFormDataStreamProvider(root);

      try
      {
        // Read the form data.
        await Request.Content.ReadAsMultipartAsync(provider);

        // This illustrates how to get the file names.
        foreach (MultipartFileData file in provider.FileData)
        {
          //Console.WriteLine(file.Headers.ContentDisposition.FileName);
          //Trace.WriteLine("Server file path: " + file.LocalFileName);
          if (File.Exists(Path.Combine(root, "test.jpg")))
            File.Delete(Path.Combine(root, "test.jpg"));

          File.Move(file.LocalFileName, Path.Combine(root, "test.jpg"));
          return Ok();
        }
      }
      catch (System.Exception e)
      {
      }
      return Ok();
    }
  }
}