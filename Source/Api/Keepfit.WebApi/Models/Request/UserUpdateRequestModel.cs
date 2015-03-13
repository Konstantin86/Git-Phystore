using System;
using System.ComponentModel.DataAnnotations;

namespace Keepfit.WebApi.Models.Request
{
  public class UserUpdateRequestModel
  {
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    public string Sex { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    [Display(Name = "Role Name")]
    public string RoleName { get; set; }
  }
}