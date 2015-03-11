using System.IO;

namespace Keepfit.WebApi.Blob
{
  public interface IBlobStorageRepository
  {
    void UploadImageFromStream(Stream stream, string fileName);

    void RemoveImage(string fileName);
  }
}