using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboard.Entities
{
    public class EnrollmentComment : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int EnrollmentCommentId { get; set; }

        public int EnrollmentId { get; set; }

        public virtual Enrollment Enrollment { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }
    }
}
