﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Phystore.WebApi.Models.Request
{
  public class UserAddRequestModel
  {
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; }

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

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }
}