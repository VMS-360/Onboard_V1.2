using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Onboard.Web.UI.Models.MaintenanceViewModels
{
    public class OnboardUserViewModel
    {
        public OnboardUserViewModel()
        {
            this.Roles = new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            this.Internals = new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            this.IsSelected = false;
        }

        public int UserId { get; set; }

        [Display(Name = "UserName")]
        [Required(ErrorMessage = "User Name is required")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Internal")]
        public bool IsInternal { get; set; }

        public bool IsSelected { get; set; }

        [Required(ErrorMessage = "Internal is required")]
        public string Internal { get; set; }

        public int? RoleId { get; set; }

        public string RoleName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Role")]
        public OnboardRoleViewModel Role { get; set; }

        public List<SelectListItem> Roles { get; set; }

        public List<SelectListItem> Internals { get; set; }
    }
}
