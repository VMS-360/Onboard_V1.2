using System.ComponentModel.DataAnnotations;

namespace Onboard.Entities
{
    public class EmploymentType : BaseEntity
    {
        [Key]
        [MaxLength(10)]
        public string EmploymentTypeCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
