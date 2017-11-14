using System.ComponentModel.DataAnnotations;


namespace Onboard.Web.UI.Models.DatabaseViewModels
{
    public class EndClientsViewModel
    {
        public int EndClientId { get; set; }

        public string Status { get; set; }

        [Display(Name = "CompanyName")]
        [Required(ErrorMessage = "Client Name is required")]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        public string CompanyName { get; set; }

        [MaxLength(100)]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(2)]
        public string State { get; set; }

        [MaxLength(11)]
        public string Zip { get; set; }

        public int ContactsCount { get; set; }

        public int ConsultantsCount { get; set; }

        public int EndClientsCount { get; set; }

        [MaxLength(10)]
        public string TaxId { get; set; }

        public string CurrentUser { get; set; }

    }
}