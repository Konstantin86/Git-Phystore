using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using Microsoft.AspNet.Identity;

using Phystore.DAL.Managers;
using Phystore.WebApi.Blob;
using Phystore.WebApi.Controllers.Base;

namespace Phystore.WebApi.Controllers
{
  namespace MyProject.Controllers
  {
    [Authorize]
    [RoutePrefix("api/blob")]
    public class BlobController : BaseApiController
    {
      private readonly IBlobStorageRepository _blobRepository;
      private readonly IFlowJsRepo _flowJs;

      public BlobController(IBlobStorageRepository blobRepository)
      {
        _blobRepository = blobRepository;
        _flowJs = new FlowJsRepo();
      }

      string _folder = HttpContext.Current.Server.MapPath("~/Images");

      [HttpGet]
      [Route("Upload")]
      public async Task<IHttpActionResult> PictureUploadGet()
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
        validationRules.MaxFileSize = 15000000;
        //validationRules.MaxFileSize = 5000000;

        try
        {
          var status = _flowJs.PostChunk(request, _folder, validationRules);

          if (status.Status == PostChunkStatus.Done)
          {
            var filePath = Path.Combine(_folder, status.FileName);
            //  byte[] file = File.ReadAllBytes(filePath);

            string bloblPhotoName = Guid.NewGuid() + "." + status.FileName.Split('.').Last();

            using (var fileStream = File.OpenRead(filePath))
            {
              _blobRepository.UploadPhotoFromStream(fileStream, bloblPhotoName);
            }

            File.Delete(filePath);

            var user = await AppUserManager.FindByNameAsync(User.Identity.Name);

            user.PhotoPath = bloblPhotoName;

            IdentityResult result = await AppUserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
              return GetErrorResult(result);
            }

            return Ok();
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