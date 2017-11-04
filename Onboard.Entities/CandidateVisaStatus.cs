using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboard.Entities
{
    public class CandidateVisaStatus : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CandidateVisaStatusId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }

        [Required]
        public string VisaTypeCode { get; set; }

        public virtual VisaType VisaType { get; set; }
    }
}
