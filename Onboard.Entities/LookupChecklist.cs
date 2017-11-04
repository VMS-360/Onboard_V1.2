using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboard.Entities
{
    public class LookupChecklist : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int LookupChecklistId { get; set; }

        [MaxLength(200)]
        public string Text { get; set; }

        [MaxLength(2)]
        public string IsActive { get; set; }

        public int? ClientId { get; set; }

        [MaxLength(10)]
        public string EmploymentType { get; set; }

        [MaxLength(10)]
        public string CommentType { get; set; }
    }
}
