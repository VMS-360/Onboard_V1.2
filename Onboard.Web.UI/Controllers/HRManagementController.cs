using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Onboard.Web.UI.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Onboard.Web.UI.Services;

namespace Onboard.Web.UI.Controllers
{
    public class HRManagementController : OnboardController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ICandidateService _candidateService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IUserLookupService _userService;

        public HRManagementController(UserManager<ApplicationUser> userManager,
                                       ICompositeViewEngine viewEngine,
                                       IEnrollmentService enrollmentService,
                                       ICandidateService candidateService,
                                       IUserLookupService userService) : base(viewEngine)
        {
            this._userManager = userManager;
            this._candidateService = candidateService;
            this._enrollmentService = enrollmentService;
            this._userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult PrepareGraph()
        {
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            var hrUsers = this._userService.GetHRUsers(loggedUser.ProductOwnerId);
            IList<int?> hrCounts = this._candidateService.GetHRPeningEnrollmentCounts(loggedUser.ProductOwnerId);
            List<string> hrEmployees = new List<string>();
            List<int> candidates = new List<int>();

            foreach (var hrUser in hrUsers)
            {
                var theUser = this._userManager.Users.Where(r => r.UserName == hrUser.Text).FirstOrDefault();
                if (theUser != null)
                {
                    hrEmployees.Add(theUser.FirstName + " " + theUser.LastName);
                    candidates.Add(hrCounts.Where(r => Convert.ToInt32(r) == Convert.ToInt32(hrUser.Value)).Count());
                }
            }

            return this.Json(
               new
               {
                   Success = true,
                   HREmployees = hrEmployees.ToArray(),
                   Candidates = candidates.ToArray()
               });
        }

        public ActionResult PrepareOnboardedGraph()
        {
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            var hrUsers = this._userService.GetHRUsers(loggedUser.ProductOwnerId);
            IList<int?> hrCounts = this._candidateService.GetHROnboardedEnrollmentCounts(loggedUser.ProductOwnerId);
            List<string> hrEmployees = new List<string>();
            List<int> candidates = new List<int>();

            foreach (var hrUser in hrUsers)
            {
                var theUser = this._userManager.Users.Where(r => r.UserName == hrUser.Text).FirstOrDefault();
                if (theUser != null)
                {
                    hrEmployees.Add(theUser.FirstName + " " + theUser.LastName);
                    candidates.Add(hrCounts.Where(r => Convert.ToInt32(r) == Convert.ToInt32(hrUser.Value)).Count());
                }
            }

            return this.Json(
               new
               {
                   Success = true,
                   HREmployees = hrEmployees.ToArray(),
                   Candidates = candidates.ToArray()
               });
        }
    }
}