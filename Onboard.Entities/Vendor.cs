using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboard.Entities
{
    public class Vendor : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int VendorId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }

        [MaxLength(10)]
        public string TaxId { get; set; }

        [MaxLength(100)]
        public string AddressLine1 { get; set; }

        [MaxLength(100)]
        public string AddressLine2 { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(2)]
        public string State { get; set; }

        [MaxLength(11)]
        public string Zip { get; set; }

        //public char WelcomeEmail { get; set; }

        //public char MSA { get; set; }

        //public char W9 { get; set; }

        //public char DirectDepositForm { get; set; }

        //public char COI { get; set; }

        //[MaxLength(10)]
        //public DateTime? COIExpiration { get; set; }

        public virtual ICollection<VendorContact> VendorContacts { get; set; }

        [Required]
        public int ProductOwnerId { get; set; }

        public virtual ProductOwner ProductOwner { get; set; }
    }
}
