using System;
using System.ComponentModel.DataAnnotations;

namespace Phystore.WebApi.Models.Request
{
  public class UserUpdateRequestModel
  {
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string Sex { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    [Display(Name = "Role Name")]
    public string RoleName { get; set; }
  }
}