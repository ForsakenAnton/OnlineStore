using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.IdentityModels.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The password must contain at least six characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords are different")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
