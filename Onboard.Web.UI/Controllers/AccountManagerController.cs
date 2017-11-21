using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Onboard.Web.UI.Services;
using Onboard.Web.UI.Models.HRViewModels;
using Microsoft.AspNetCore.Identity;
using Onboard.Web.UI.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Onboard.Web.UI.Models.DatabaseViewModels;

namespace Onboard.Web.UI.Controllers
{
    public class AccountManagerController : OnboardController
    {
        private readonly ICandidateService _candidateService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly IUserLookupService _userService;
        private readonly ILookupService _lookupService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IChecklistService _checklistService;
        private readonly IDatabaseService _databaseService;


        public AccountManagerController(ICandidateService candidateService,
                                       UserManager<ApplicationUser> userManager,
                                       ICompositeViewEngine viewEngine,
                                       IUserLookupService userService,
                                       ILookupService lookupService,
                                       IEnrollmentService enrollmentService,
                                       IChecklistService checklistService,
                                       IDatabaseService databaseService) : base(viewEngine)

        {
            this._candidateService = candidateService;
            this._userManager = userManager;
            this._userService = userService;
            this._lookupService = lookupService;
            this._enrollmentService = enrollmentService;
            this._checklistService = checklistService;
            this._databaseService = databaseService;
        }

        public IActionResult Index()
        {
            ManagementViewModel model = new ManagementViewModel();
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            model.PendingCandidates = this._candidateService.GetMyCandidates(loggedUser.Id, loggedUser.ProductOwnerId);
            model.RecentOnboards = this._candidateService.GetOnboardCandidates(loggedUser.Id, loggedUser.ProductOwnerId).Where(r => r.OnboardedDate >= DateTime.Now.AddDays(-30)).ToList();
            model.DeclinedCandidates = this._candidateService.GetDeclinedCandidates(loggedUser.ProductOwnerId);
            //model.AssignedCandidates = this._candidateService.GetMyCandidates(loggedUser.Id, loggedUser.ProductOwnerId);

            var test = _lookupService.GetTaxStatuses();

            return View(model);
        }

        public IActionResult PrepareCandiateDetails(string enrollmentId)
        {
            CandidateDetailsViewModel viewModel = new CandidateDetailsViewModel();
            if (enrollmentId != null && enrollmentId.Length > 0)
            {
                viewModel = this._candidateService.GetCandateDetails(Convert.ToInt32(enrollmentId));
            }

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_CandidateDetails", viewModel)
                            });
        }
    }
}