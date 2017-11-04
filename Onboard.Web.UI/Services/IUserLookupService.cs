using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Onboard.Web.UI.Services
{
    public interface IUserLookupService
    {
        List<SelectListItem> GetPortfolioManagers(int productOwnerId);
        List<SelectListItem> GetHRUsers(int productOwnerId);
    }
}