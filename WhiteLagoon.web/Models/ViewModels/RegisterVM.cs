using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace WhiteLagoon.web.Models.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [Display(Name = " Name")]

        public string Name { get; set; }
        [Required]
        [Display(Name = " Email")]

        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        [Display(Name = " Confirm Password")]

        public string ConfirmPassword { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string RedirectUrl { get; set; }
        [ValidateNever]

        public IEnumerable<SelectListItem> RoleList { get; set; }
        public string Role { get; set; }

    }
}
