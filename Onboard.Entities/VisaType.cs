using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onboard.Entities
{
    public class VisaType : BaseEntity
    {
        [Key]
        [MaxLength(10)]
        public string VisaTypeCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        public virtual ICollection<CandidateVisaStatus> CandidateVisaStatuses { get; set; }
    }
}
