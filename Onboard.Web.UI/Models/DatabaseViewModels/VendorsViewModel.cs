using System.ComponentModel.DataAnnotations;

namespace Onboard.Web.UI.Models.DatabaseViewModels
{
    public class VendorsViewModel
    {
        public int VendorId { get; set; }

        public string Status { get; set; }

        [Display(Name = "CompanyName")]
        [Required(ErrorMessage = "Vendor Name is required")]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [DataType(DataType.Text)]
        [MaxLength(2)]
        public string State { get; set; }

        [Required(ErrorMessage = "Zip is required")]
        [DataType(DataType.Text)]
        [MaxLength(11)]
        public string Zip { get; set; }

        public int ConsultantsCount { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(10)]
        public string TaxId { get; set; }

        public string CurrentUser { get; set; }

        public string SignatoryName { get; set; }
        public string SignatoryEmail { get; set; }
        public string SignatoryPhone { get; set; }
        public string SignatoryExtn { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactExtn { get; set; }
    }
}
