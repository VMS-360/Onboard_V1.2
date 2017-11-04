using System.ComponentModel.DataAnnotations;

namespace Onboard.Web.UI.Models.HRViewModels
{
    public class CommentViewModel
    {
        [Required(ErrorMessage = "Enrollment Id is required")]
        public int CommentEnrollmentId { get; set; }

        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Comment Name is required")]
        [MaxLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        public string CurrentUser { get; set; }
    }
}
