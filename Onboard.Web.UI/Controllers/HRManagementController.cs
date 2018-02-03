using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Onboard.Web.UI.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Onboard.Web.UI.Services;
using Kendo.Mvc.UI;
using Onboard.Web.UI.Models.HRViewModels;
using Kendo.Mvc.Extensions;

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
            CandidateSpecViewModel model = new CandidateSpecViewModel();
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();

            List<List<string>> onboardedModel = new List<List<string>>();
            List<DateTime> template = new List<DateTime>();
            List<string> years = new List<string>();
            years.Add("Name");
            template.Add(new DateTime());
        
            for (int i = 0; i < 12; i++)
            {
                var currentDate = DateTime.Now.AddMonths(i * -1);
                years.Add(this.ExtractDateString(currentDate));
                template.Add(new DateTime(currentDate.Year, currentDate.Month, 1));
            }

            var hrUsers = this._userService.GetHRUsers(loggedUser.ProductOwnerId);
            List<string> hrs = new List<string>();
            hrs.Add("Name");
            for (int i = 12; i > 0; i--)
            {
                var curDate = template[i];
                hrs.Add(curDate.ToShortDateString());
            }

            onboardedModel.Add(hrs);
            foreach (var hrUser in hrUsers)
            {
                var onboarded = this._candidateService.GetOnboardCandidates(Convert.ToInt32(hrUser.Value), loggedUser.ProductOwnerId);
                hrs = new List<string>();

                var theUser = this._userManager.Users.Where(r => r.UserName == hrUser.Text).FirstOrDefault();
                if (theUser != null)
                {
                    hrs.Add(theUser.FirstName + " " + theUser.LastName);
                }

                for (int i = 12; i > 0; i--)
                {
                    int count = onboarded.Where(r => template[i] <= r.OnboardedDate && r.OnboardedDate< template[i].AddMonths(1)).Count();
                    hrs.Add(count.ToString());
                }

                onboardedModel.Add(hrs);
            }

            model.Onboarded = onboardedModel;

            return View(model);
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

        public ActionResult GetAllEmployeesList([DataSourceRequest] DataSourceRequest request)
        {
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            var hrUsers = this._userService.GetHRUsers(loggedUser.ProductOwnerId);
            var allCandidates = this._candidateService.GetAllPendingCandidates(loggedUser.ProductOwnerId);

            List<AllEmployeesViewModel> employees = new List<AllEmployeesViewModel>();

            AllEmployeesViewModel theEmployee;
            foreach (var hrUser in hrUsers)
            {
                theEmployee = new AllEmployeesViewModel();

                var theUser = this._userManager.Users.Where(r => r.UserName == hrUser.Text).FirstOrDefault();
                if (theUser != null)
                {
                    theEmployee.EmployeeName = theUser.FirstName + " " + theUser.LastName;
                }

                var candidates = allCandidates.Where(r => r.HRUserId == Convert.ToInt32(hrUser.Value));
                foreach(var candidate in candidates)
                {
                    theEmployee.EnrollmentId = candidate.EnrollmentId;
                    theEmployee.CandidateName = candidate.ConsultantName;
                    theEmployee.ClietName = candidate.ClientName;
                    theEmployee.TaxStatus = candidate.TaxStatusString;
                    theEmployee.LastUpdated =  candidate.ModifiedDate == null ? candidate.CreatedDate : candidate.ModifiedDate;
                    theEmployee.EnrollmentId = candidate.EnrollmentId;
                }

                if (theEmployee.EnrollmentId != 0)
                {
                    employees.Add(theEmployee);
                }
            }

            return Json(employees.ToDataSourceResult(request));
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

        public IActionResult PrepareEnrollmentAssign(string enrollmentId)
        {
            AssignEnrollemntViewModel model = new AssignEnrollemntViewModel();
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            model.EnrollmentId = enrollmentId;
            model.HRUsers = this._userService.GetHRUsers(loggedUser.ProductOwnerId);

            foreach(var theUser in model.HRUsers)
            {
                var user = this._userManager.Users.Where(r => r.Id ==  Convert.ToInt32(theUser.Value)).FirstOrDefault();

                theUser.Text = user.FirstName + " " + user.LastName;
            }

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_Assign", model)
                            });
        }

        public IActionResult EnrollmentAssign(AssignEnrollemntViewModel model)
        {
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            if (model != null && ModelState.IsValid)
            {
                try
                {
                    this._candidateService.AssignEnrollment(Convert.ToInt32(model.EnrollmentId), Convert.ToInt32(model.HRUser), User.Identity.Name);

                    return this.Json(
                               new
                               {
                                   Success = true,
                                   Message = "Saved Successfully."
                               });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("HRUser", e.Message);
                }

                return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_Assign", model)
                            });
            }
            else
            {
                return this.GetJsonResult(false, "Validation failed", this.ModelErrors());
            }
        }

        private string ExtractDateString(DateTime date)
        {
            return date.ToString("MMM yy");
        }
    }
}