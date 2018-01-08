using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Onboard.Web.UI.Models;
using Microsoft.AspNetCore.Identity;
using Onboard.Web.UI.Models.HomeViewModels;
using Onboard.Web.UI.Services;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Onboard.Web.UI.Controllers
{
    public class HomeController : OnboardController
    {
        private readonly ICandidateService _candidateService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompositeViewEngine _viewEngine;

        public HomeController(ICandidateService candidateService, UserManager<ApplicationUser> userManager, 
            ICompositeViewEngine viewEngine) : base(viewEngine)
        {
            this._candidateService = candidateService;
            this._userManager = userManager;
        }

        [Authorize(Roles = "Admin, HR, Account Manager, Portfolio, Accounting, Executive")]
        public IActionResult Index()
        {
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            DashboardViewModel model = new DashboardViewModel();
            if(loggedUser !=null)
            {
                model.FirstName = loggedUser.FirstName;
                model.LastName = loggedUser.LastName;
            }

            model.UnassignedCount = this._candidateService.GetPendingCandidates(loggedUser.ProductOwnerId).Count();
            model.PendingCount = this._candidateService.GetMyCandidates(loggedUser.Id, loggedUser.ProductOwnerId).Count();

            model.UsassignedCliclable = model.UnassignedCount > 0 && (User.IsInRole("Admin") || User.IsInRole("HR")) ? "Y": "N";
            model.PendingCliclable = model.PendingCount > 0 && (User.IsInRole("Admin") || User.IsInRole("HR")) ? "Y" : "N";

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public ActionResult PrepareGraph()
        {
            List<string> years = new List<string>();
            for (int i = 0; i < 12; i++)
            {
                years.Add(this.ExtractDateString(DateTime.Now.AddMonths(i * -1)));
            }

            IList<DateTime?> onboardedDates = new List<DateTime?>();

            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            onboardedDates = this._candidateService.GetOnboardDates(loggedUser.ProductOwnerId);

            List<int> candidates = new List<int>();

            for (int i = 0; i < 12; i++)
            {
                DateTime graphDay = DateTime.Now.AddMonths(i * -1);

                DateTime firstDayOfMonth = new DateTime(graphDay.Year, graphDay.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                candidates.Add(onboardedDates.Where(r => r >= firstDayOfMonth && r <= lastDayOfMonth).Count());
            }

            years.Reverse();
            candidates.Reverse();

            return this.Json(
               new
               {
                   Success = true,
                   Years = years.ToArray(),
                   Candidates = candidates.ToArray()
               });
        }

        private string ExtractDateString(DateTime date)
        {
            return date.ToString("MMM yy");
        }
    }
}
