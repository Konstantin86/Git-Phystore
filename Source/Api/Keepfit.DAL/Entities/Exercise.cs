using System.ComponentModel.DataAnnotations;
using Keepfit.DAL.Entities.Base;

namespace Keepfit.DAL.Entities
{
  public class Exercise : EntityBase
  {
    [Required]
    public string Name { get; set; } 

    public string Description { get; set; } 

    public string Category { get; set; } 
  }
}