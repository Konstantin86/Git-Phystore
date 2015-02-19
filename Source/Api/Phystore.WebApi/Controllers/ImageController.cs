using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
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

          // Storage saving
          // http://az725561.vo.msecnd.net/ (TODO obtain CDN using .net azure api)
          CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

          CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

          CloudBlobContainer container = blobClient.GetContainerReference("media");
          // TODO First time get images from blob storage, then from cdn
          // Use Guid to generate name:
          CloudBlockBlob blockBlob = container.GetBlockBlobReference("22.jpg");
          //CloudBlockBlob blockBlob = container.GetBlockBlobReference(file.Headers.ContentDisposition.FileName);
          //blockBlob.Properties.ContentType = "image/jpg";
          blockBlob.Properties.ContentType = file.Headers.ContentType.MediaType;
          //blockBlob.Properties.ContentType = file.Headers.ContentType.MediaType;
          //blockBlob.SetProperties();

          using (var fileStream = System.IO.File.OpenRead(file.LocalFileName))
          {
            blockBlob.UploadFromStream(fileStream);
          } 

          // End
          // more detailed documentation on working with blobs is here - http://azure.microsoft.com/uk-ua/documentation/articles/storage-dotnet-how-to-use-blobs/

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