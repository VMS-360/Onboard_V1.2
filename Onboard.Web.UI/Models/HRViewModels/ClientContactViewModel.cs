using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onboard.Web.UI.Models.HRViewModels
{
    public class ClientContactViewModel
    {
        public int ClientContactId { get; set; }

        [Required(ErrorMessage = "Client is required")]
        public int ClientId { get; set; }

        public string ClientName { get; set; }

        [Required(ErrorMessage = "Contact Name is required")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        public string Phone { get; set; }

        public string CurrentUser { get; set; }
    }
}