using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Onboard.Web.UI.Models;
using System.Collections.Generic;
using System.Linq;

namespace Onboard.Web.UI.Services
{
    public class UserLookupService : IUserLookupService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserLookupService(UserManager<ApplicationUser> userManager,
                                    RoleManager<ApplicationRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public List<SelectListItem> GetPortfolioManagers(int productOwnerId)
        {
            return this.GetUsersForRole(productOwnerId, "Portfolio");
        }

        public List<SelectListItem> GetHRUsers(int productOwnerId)
        {
            return this.GetUsersForRole(productOwnerId, "HR");
        }

        private List<SelectListItem> GetUsersForRole(int productOwnerId, string role)
        {
            List<SelectListItem> returnList = new List<SelectListItem>();
            var theRole = this._roleManager.Roles.Where(r => r.Name == role || r.Name.ToLower() == "admin").FirstOrDefault();

            if (theRole != null)
            {
                var userRoles = this._userManager
                                 .Users
                                 .Include(r => r.Roles)
                                 .Select(r => new { Text = r.UserName, Value = r.Id.ToString(), Roles = r.Roles, Test = r.Roles.Select(x => x.RoleId).ToList() });

                returnList = userRoles.Where(r => r.Test.Contains(theRole.Id)).Select(r => new SelectListItem { Text = r.Text, Value = r.Value }).ToList();
            }

            return returnList;
        }
    }
}
