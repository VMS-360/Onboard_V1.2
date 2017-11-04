using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboard.Entities
{
    public class Client : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ClientId { get; set; }

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

        public virtual ICollection<ClientContact> ClientContacts { get; set; }

        [Required]
        public int ProductOwnerId { get; set; }

        public virtual ProductOwner ProductOwner { get; set; }
    }
}
