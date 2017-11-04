using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboard.Entities
{
    public class VendorChecklist : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int VendorChecklistId { get; set; }

        [Required]
        public int VendorId { get; set; }

        [MaxLength(200)]
        public string Text { get; set; }

        [MaxLength(2)]
        public string IndCompleted { get; set; }

        [MaxLength(2)]
        public string IsActive { get; set; }

        [MaxLength(10)]
        public string CommentType { get; set; }

        [MaxLength(30)]
        public string CommentValue { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
