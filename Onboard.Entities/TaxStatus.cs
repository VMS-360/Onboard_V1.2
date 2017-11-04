using System.ComponentModel.DataAnnotations;

namespace Onboard.Entities
{
    public class TaxStatus : BaseEntity
    {
        [Key]
        [MaxLength(10)]
        public string TaxStatusCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
