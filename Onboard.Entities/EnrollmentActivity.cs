using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboard.Entities
{
    public class EnrollmentActivity : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int EnrollmentActivityId { get; set; }

        public int EnrollmentId { get; set; }

        public virtual Enrollment Enrollment { get; set; }

        [MaxLength(500)]
        public string Action { get; set; }
    }
}
