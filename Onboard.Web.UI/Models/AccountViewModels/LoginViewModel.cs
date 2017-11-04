using System.ComponentModel.DataAnnotations;

namespace Onboard.Web.UI.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "username is required")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
