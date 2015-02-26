using System.ComponentModel.DataAnnotations;
using Phystore.DAL.Entities.Base;

namespace Phystore.DAL.Entities
{
  public class Exercise : EntityBase
  {
    [Required]
    public string Name { get; set; } 

    public string Description { get; set; } 

    public string Category { get; set; } 
  }
}