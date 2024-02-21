using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD

=======
>>>>>>> 5e043480213227ff2df602398908183439f8c929
namespace WhiteLagoon.web.Models.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [Display(Name = " Name")]
<<<<<<< HEAD

        public string Name { get; set; }
        [Required]
        [Display(Name = " Email")]

        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]

=======
        public string Name { get; set; }
        [Required]
        [Display(Name = " Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
>>>>>>> 5e043480213227ff2df602398908183439f8c929
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        [Display(Name = " Confirm Password")]
<<<<<<< HEAD

=======
>>>>>>> 5e043480213227ff2df602398908183439f8c929
        public string ConfirmPassword { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string RedirectUrl { get; }
        [ValidateNever]
<<<<<<< HEAD

        public IEnumerable<SelectListItem> RoleList { get; set; }
        public string Role { get; set; }

=======
        public IEnumerable<SelectListItem> RoleList { get; set; }
        public string Role { get; set; }
>>>>>>> 5e043480213227ff2df602398908183439f8c929
    }
}
