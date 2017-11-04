using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboard.Entities
{
    public class Enrollment : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int EnrollmentId { get; set; }

        public string Internal { get; set; }

        public int? ProtfolioManagerId { get; set; }

        public int? HRUserId { get; set; }

        [MaxLength(80)]
        public string JobTitle { get; set; }

        public int? DurationYears { get; set; }

        public int? DurationMonths { get; set; }

        public decimal? BillRate { get; set; }

        public decimal? PayRate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string BGVRequired { get; set; }

        public string BGVCompany { get; set; }

        public string DrugTestRequired { get; set; }

        public string DrugTestCompany { get; set; }

        public string VendorPO { get; set; }

        public string OnboardedIndicator { get; set; }

        public DateTime? OnboardedDate { get; set; }

        public string ActiveIndicator { get; set; }

        public DateTime? InactiveDate { get; set; }

        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }

        public string EmploymentTypeCode { get; set; }

        public virtual EmploymentType EmploymentType { get; set; }

        public string TaxStatusCode { get; set; }

        public virtual TaxStatus TaxStatus { get; set; }

        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }

        public int? ClientContactId { get; set; }

        public virtual ClientContact ClientContact { get; set; }

        public int? EndClientId { get; set; }

        public virtual EndClient EndClient { get; set; }

        public int? VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }

        public int? VendorContactId { get; set; }

        public virtual VendorContact VendorContact { get; set; }
    }
}
