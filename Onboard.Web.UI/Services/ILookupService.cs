using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Onboard.Web.UI.Services
{
    public interface ILookupService
    {
        List<SelectListItem> GetClientContacts(int clientId);
        List<SelectListItem> GetClients(int productOwnerId);
        List<SelectListItem> GetDurationMonths();
        List<SelectListItem> GetDurationYears();
        List<SelectListItem> GetEndClients(int productOwnerId);
        List<SelectListItem> GetTaxStatuses();
        List<SelectListItem> GetVendors(int productOwnerId);
        List<SelectListItem> GetVisaTypes();
    }
}