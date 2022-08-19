using System.ComponentModel.DataAnnotations;

namespace Assets.Core.Identity.Service.ViewModel.UI
{
    public class RegisterInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Please make sure your passwords match")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
