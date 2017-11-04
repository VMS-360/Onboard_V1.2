using Microsoft.AspNetCore.Mvc.Rendering;
using Onboard.Web.UI.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onboard.Web.UI.Services
{
    public class LookupService : ILookupService
    {
        private readonly OnboardDb _context;

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
    }
}
