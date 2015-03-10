using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ImageResizer;
using Keepfit.WebApi.Blob;
using Keepfit.WebApi.Controllers.Base;
using Microsoft.AspNet.Identity;

namespace Keepfit.WebApi.Controllers
{
  namespace MyProject.Controllers
  {
    [Authorize]
    [RoutePrefix("api/blob")]
    public class BlobController : BaseApiController
    {
      private readonly IBlobStorageRepository _blobRepository;
      private readonly IFlowJsRepo _flowJs;

      readonly string _folder = HttpContext.Current.Server.MapPath("~/Images");

      public BlobController(IBlobStorageRepository blobRepository)
      {
        _blobRepository = blobRepository;
        _flowJs = new FlowJsRepo();
      }

      [HttpGet]
      [Route("Upload")]
      public IHttpActionResult PictureUploadGet()
      {
        var request = HttpContext.Current.Request;

        var chunkExists = _flowJs.ChunkExists(_folder, request);
        if (chunkExists) return Ok();
        return NotFound();
      }

      [HttpPost]
      [Route("Upload")]
      public async Task<IHttpActionResult> PictureUploadPost()
      {
        var request = HttpContext.Current.Request;

        var validationRules = new FlowValidationRules();
        validationRules.AcceptedExtensions.AddRange(new List<string> { "jpeg", "jpg", "png", "bmp" });
        validationRules.MaxFileSize = 50000000;

        try
        {
          var status = _flowJs.PostChunk(request, _folder, validationRules);

          if (status.Status == PostChunkStatus.Done)
          {
            var filePath = Path.Combine(_folder, status.FileName);

            string extension = ImageResizer.Util.PathUtils.GetExtension(filePath);
            string basePath = ImageResizer.Util.PathUtils.RemoveExtension(filePath);

            Dictionary<string, string> versions = new Dictionary<string, string>();
            //Define the versions to generate and their filename suffixes.
            //versions.Add("_thumb", "width=100&height=100&crop=auto&format=jpg"); //Crop to square thumbnail
            //versions.Add("_medium", "maxwidth=400&maxheight=400format=jpg"); //Fit inside 400x400 area, jpeg
            //versions.Add("_large", "maxwidth=1900&maxheight=1900&format=jpg"); //Fit inside 1900x1200 area
            
            versions.Add("_ava", "maxwidth=" + ConfigurationManager.AppSettings["userPhotoMaxWidth"] + "&format=jpg"); //Fit inside 360 pixels by width (method is supposed for uploading user profile photos (avatars)

            string fileName = Guid.NewGuid() + "." + status.FileName.Split('.').Last();

            foreach (string suffix in versions.Keys)
            {
              ImageBuilder.Current.Build(filePath, basePath + suffix + extension, new ResizeSettings(versions[suffix]));
            }

            string userPhotoPath = basePath + "_ava" + extension;
            //string userPhotoPath = filePath;

            using (var fileStream = File.OpenRead(userPhotoPath))
            {
              _blobRepository.UploadPhotoFromStream(fileStream, fileName);
            }

            File.Delete(filePath);
            File.Delete(userPhotoPath);

            var user = await AppUserManager.FindByNameAsync(User.Identity.Name);

            user.PhotoPath = fileName;

            IdentityResult result = await AppUserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
              return GetErrorResult(result);
            }

            return Ok(fileName);
          }

          if (status.Status == PostChunkStatus.PartlyDone)
          {
            return Ok();
          }

          status.ErrorMessages.ForEach(x => ModelState.AddModelError("file", x));
          return BadRequest(ModelState);
        }
        catch (Exception)
        {
          ModelState.AddModelError("file", "exception");
          return BadRequest(ModelState);
        }
      }
    }
  }
}