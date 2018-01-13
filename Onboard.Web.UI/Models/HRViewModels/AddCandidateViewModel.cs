using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onboard.Web.UI.Models.HRViewModels
{
    public class AddCandidateViewModel
    {
        [Required(ErrorMessage = "Internal is required")]
        [DataType(DataType.Text)]
        public string Internal { get; set; }

        [Required(ErrorMessage = "Tax Status is required")]
        [DataType(DataType.Text)]
        public string TaxStatus { get; set; }

        [DataType(DataType.Text)]
        public string PortfolioManager { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Visa Status is required")]
        [DataType(DataType.Text)]
        public string VisaStatus { get; set; }

        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Visa Expiration is required")]
        [DataType(DataType.Date)]
        public string VisaExpiration { get; set; }

        [DataType(DataType.Text)]
        public string City { get; set; }

        [DataType(DataType.Text)]
        public string State { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(5, ErrorMessage = "Zip should be 5 characters long")]
        public string Zip { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(11, ErrorMessage ="SSN should be 9 characters long")]
        public string SSN { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Client is required")]
        public string Client { get; set; }

        public string EndClient { get; set; }

        public string ClientContact { get; set; }

        public string EndContact { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Job Title is required")]
        public string JobTitle { get; set; }

        public string Duration { get; set; }

        [DataType(DataType.Currency)]
        public string BillRate { get; set; }

        [Required(ErrorMessage = "Pay Rate is required")]
        [DataType(DataType.Currency)]
        public string PayRate { get; set; }

        public string Vendor { get; set; }

        public string VendorContact { get; set; }

        public List<SelectListItem> PortfolioManagers { get; set; }
        public List<SelectListItem> ClientContacts { get; set; }
        public List<SelectListItem> Clients { get; set; }
        //public List<SelectListItem> DurationMonths { get; set; }
        //public List<SelectListItem> DurationYears { get; set; }
        public List<SelectListItem> EndClients { get; set; }
        public List<SelectListItem> TaxStatuses { get; set; }
        public List<SelectListItem> Vendors { get; set; }
        public List<SelectListItem> VisaTypes { get; set; }
        public List<SelectListItem> States { get; set; }
        public string EmploymentTypeCode { get; internal set; }
        public char Gender { get; internal set; }
    }
}
