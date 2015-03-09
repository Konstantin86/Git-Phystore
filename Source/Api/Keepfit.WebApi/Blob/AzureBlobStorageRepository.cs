using System.IO;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Keepfit.WebApi.Blob
{
  public class AzureBlobStorageRepository : IBlobStorageRepository
  {
    public void UploadPhotoFromStream(Stream stream, string fileName)
    {
      CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
      CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
      CloudBlobContainer container = blobClient.GetContainerReference("media");
      CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

      // TODO ugly code - should be changed with real content media type obtained from http request of multi-part data: smt like this - file.Headers.ContentType.MediaType
      string format = fileName.Split('.').Last();
      
      if (format == "jpg")
      {
        format = "jpeg";
      }

      blockBlob.Properties.ContentType = "image/" + format;

      blockBlob.UploadFromStream(stream);
    }
  }
}