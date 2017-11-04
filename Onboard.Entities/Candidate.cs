using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboard.Entities
{
    public class Candidate : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CandidateId { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(20)]
        public string MI { get; set; }

        public string Gender { get; set; }

        [MaxLength(9)]
        public string SSN { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string AddressLine1 { get; set; }

        [MaxLength(100)]
        public string AddressLine2 { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(20)]
        public string State { get; set; }

        [MaxLength(10)]
        public string Zip { get; set; }

        [MaxLength(12)]
        public string Phone { get; set; }

        [MaxLength(12)]
        public string Phone2 { get; set; }

        public virtual ICollection<CandidateVisaStatus> CandidateVisaStatuses { get; set; }

        [Required]
        public int ProductOwnerId { get; set; }

        public virtual ProductOwner ProductOwner { get; set; }
    }
}
