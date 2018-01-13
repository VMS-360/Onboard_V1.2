using Microsoft.AspNetCore.Mvc.Rendering;
using Onboard.Web.UI.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Onboard.Web.UI.Services
{
    public class LookupService : ILookupService
    {
        private readonly OnboardDb _context;

        public String SendEmailAsync(string email, string subject, string message)
        {


            string from = email;
            string to = email;
            //string subject = "This is the subject";
            string plainTextBody = "This is a great message.";
            MailMessage mailMessage = new MailMessage(from, to, subject, plainTextBody);
            string smtpServer = "smtp.gmail.com";
            SmtpClient client = new SmtpClient(smtpServer);
             client.Send(mailMessage);
            return "";



        }

        public LookupService(OnboardDb context)
        {
            this._context = context;
        }

        public List<SelectListItem> GetTaxStatuses()
        {
            return this._context
                       .Ref_Tax_Status
                       .OrderBy(r => r.Description)
                       .Select(r => new SelectListItem { Text = r.Description, Value = r.TaxStatusCode })
                       .ToList();
        }

        public List<SelectListItem> GetVisaTypes()
        {
            return this._context
                       .Ref_Visa_Type
                       .OrderBy(r => r.Description)
                       .Select(r => new SelectListItem { Text = r.Description, Value = r.VisaTypeCode })
                       .ToList();
        }

        public List<SelectListItem> GetClients(int productOwnerId)
        {
            return this._context
                       .Client
                       .Where(r=> r.ProductOwnerId == productOwnerId)
                       .OrderBy(r => r.CompanyName)
                       .Select(r => new SelectListItem { Text = r.CompanyName, Value = r.ClientId.ToString() })
                       .ToList();
        }

        public List<SelectListItem> GetClientContacts(int clientId)
        {
            return this._context
                       .Client_Contact
                       .Where(r => r.ClientId == clientId)
                       .OrderBy(r => r.Name)
                       .Select(r => new SelectListItem { Text = r.Name, Value = r.ClientContactId.ToString() })
                       .ToList();
        }

        public List<SelectListItem> GetEndClients(int productOwnerId)
        {
            return this._context
                       .End_Client
                       .Where(r => r.ProductOwnerId == productOwnerId)
                       .OrderBy(r => r.CompanyName)
                       .Select(r => new SelectListItem { Text = r.CompanyName, Value = r.EndClientId.ToString() })
                       .ToList();
        }

        public List<SelectListItem> GetDurationYears()
        {
            return new List<SelectListItem>();
        }

        public List<SelectListItem> GetDurationMonths()
        {
            return new List<SelectListItem>();
        }

        public List<SelectListItem> GetVendors(int productOwnerId)
        {
            return this._context
                       .Vendor
                       .Where(r => r.ProductOwnerId == productOwnerId)
                       .OrderBy(r => r.CompanyName)
                       .Select(r => new SelectListItem { Text = r.CompanyName, Value = r.VendorId.ToString() })
                       .ToList();
        }

        public List<SelectListItem> GetStates()
        {
            List<SelectListItem> states = new List<SelectListItem>(){
                new SelectListItem { Value = "AL", Text = "Alabama"},
                new SelectListItem { Value = "AK", Text = "Alaska"},
                new SelectListItem { Value = "AZ", Text = "Arizona"},
                new SelectListItem { Value = "AR", Text = "Arkansas"},
                new SelectListItem { Value = "CA", Text = "California"},
                new SelectListItem { Value = "CO", Text = "Colorado"},
                new SelectListItem { Value = "CT", Text = "Connecticut"},
                new SelectListItem { Value = "DC", Text = "District of Columbia"},
                new SelectListItem { Value = "DE", Text = "Delaware"},
                new SelectListItem { Value = "FL", Text = "Florida"},
                new SelectListItem { Value = "GA", Text = "Georgia"},
                new SelectListItem { Value = "HI", Text = "Hawaii"},
                new SelectListItem { Value = "ID", Text = "Idaho"},
                new SelectListItem { Value = "IL", Text = "Illinois"},
                new SelectListItem { Value = "IN", Text = "Indiana"},
                new SelectListItem { Value = "IA", Text = "Iowa"},
                new SelectListItem { Value = "KS", Text = "Kansas"},
                new SelectListItem { Value = "KY", Text = "Kentucky"},
                new SelectListItem { Value = "LA", Text = "Louisiana"},
                new SelectListItem { Value = "ME", Text = "Maine"},
                new SelectListItem { Value = "MD", Text = "Maryland"},
                new SelectListItem { Value = "MA", Text = "Massachusetts"},
                new SelectListItem { Value = "MI", Text = "Michigan"},
                new SelectListItem { Value = "MN", Text = "Minnesota"},
                new SelectListItem { Value = "MS", Text = "Mississippi"},
                new SelectListItem { Value = "MO", Text = "Missouri"},
                new SelectListItem { Value = "MT", Text = "Montana"},
                new SelectListItem { Value = "NE", Text = "Nebraska"},
                new SelectListItem { Value = "NV", Text = "Nevada"},
                new SelectListItem { Value = "NH", Text = "New Hampshire"},
                new SelectListItem { Value = "NJ", Text = "New Jersey"},
                new SelectListItem { Value = "NM", Text = "New Mexico"},
                new SelectListItem { Value = "NY", Text = "New York"},
                new SelectListItem { Value = "NC", Text = "North Carolina"},
                new SelectListItem { Value = "ND", Text = "North Dakota"},
                new SelectListItem { Value = "OH", Text = "Ohio"},
                new SelectListItem { Value = "OK", Text = "Oklahoma"},
                new SelectListItem { Value = "OR", Text = "Oregon"},
                new SelectListItem { Value = "PA", Text = "Pennsylvania"},
                new SelectListItem { Value = "SC", Text = "South Carolina"},
                new SelectListItem { Value = "SD", Text = "South Dakota"},
                new SelectListItem { Value = "TN", Text = "Tennessee"},
                new SelectListItem { Value = "TX", Text = "Texas"},
                new SelectListItem { Value = "UT", Text = "Utah"},
                new SelectListItem { Value = "VT", Text = "Vermont"},
                new SelectListItem { Value = "VA", Text = "Virginia"},
                new SelectListItem { Value = "WA", Text = "Washington"},
                new SelectListItem { Value = "WV", Text = "West Virginia"},
                new SelectListItem { Value = "WI", Text = "Wisconsin"},
                new SelectListItem { Value = "WY", Text = "Wyoming"}
            };

            return states;
        }
    }
}
