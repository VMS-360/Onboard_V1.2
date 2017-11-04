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

        public HomeController(ICandidateService candidateService, UserManager<ApplicationUser> userManager, ICompositeViewEngine viewEngine) : base(viewEngine)
        {
            this._candidateService = candidateService;
            this._userManager = userManager;
        }

        [Authorize(Roles ="Admin")]
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
    }
}
