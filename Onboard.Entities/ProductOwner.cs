using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onboard.Entities
{
    public class ProductOwner : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductOwnerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
