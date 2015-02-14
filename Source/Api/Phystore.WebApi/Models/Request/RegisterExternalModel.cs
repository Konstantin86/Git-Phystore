using System.ComponentModel.DataAnnotations;

namespace Phystore.WebApi.Models.Request
{
  public class RegisterExternalModel
  {
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Provider { get; set; }

    [Required]
    public string ExternalAccessToken { get; set; } 
  }
}