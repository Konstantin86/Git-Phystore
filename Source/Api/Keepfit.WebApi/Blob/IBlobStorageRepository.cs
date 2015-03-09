using System.IO;

namespace Keepfit.WebApi.Blob
{
  public interface IBlobStorageRepository
  {
    void UploadPhotoFromStream(Stream stream, string fileName);
  }
}