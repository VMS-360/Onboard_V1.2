using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onboard.Web.UI.Models.HRViewModels
{
    public class AssignEnrollemntViewModel
    {
        [Required(ErrorMessage = "Enrollment Id is required")]
        public string EnrollmentId { get; set; }

        [Required(ErrorMessage = "Assigned To is required")]
        [DataType(DataType.Text)]
        public string HRUser { get; set; }

        public List<SelectListItem> HRUsers { get; set; }
    }
}
