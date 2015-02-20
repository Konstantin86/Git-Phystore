using System.IO;

namespace Phystore.WebApi.Blob
{
  public interface IBlobStorageRepository
  {
    void UploadPhotoFromStream(Stream stream, string fileName);
  }
}