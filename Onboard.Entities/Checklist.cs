using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboard.Entities
{
    public class Checklist : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ChecklistId { get; set; }

        [MaxLength(200)]
        public string Text { get; set; }

        [MaxLength(2)]
        public string IndCompleted { get; set; }

        [MaxLength(2)]
        public string IsActive { get; set; }

        [Required]
        public int EnrollmentId { get; set; }

        public virtual Enrollment Enrollment { get; set; }

        [MaxLength(10)]
        public string CommentType { get; set; }

        [MaxLength(30)]
        public string CommentValue { get; set; }

        [MaxLength(2)]
        public string IndVendor{ get; set; }
    }
}
