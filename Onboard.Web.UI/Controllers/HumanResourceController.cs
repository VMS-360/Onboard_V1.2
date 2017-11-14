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
    public class HumanResourceController : OnboardController
    {
        private readonly ICandidateService _candidateService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly IUserLookupService _userService;
        private readonly ILookupService _lookupService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IChecklistService _checklistService;
        private readonly IDatabaseService _databaseService;

        public HumanResourceController(ICandidateService candidateService,
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
            return View();
        }

        public IActionResult Management()
        {
            ManagementViewModel model = new ManagementViewModel();
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            model.PendingCandidates = this._candidateService.GetPendingCandidates(loggedUser.ProductOwnerId);
            model.AssignedCandidates = this._candidateService.GetMyCandidates(loggedUser.Id, loggedUser.ProductOwnerId);
            model.RecentOnboards = this._candidateService.GetOnboardCandidates(loggedUser.Id, loggedUser.ProductOwnerId);

            var test = _lookupService.GetTaxStatuses();

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateEnrollmentTaskList(string id, string state, string type, string val, string tagHelper, string enrollmentId)
        {
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            this._checklistService.UpdateEnrollmentChecklistItem(Convert.ToInt32(id), state == "Y" ? true : false, type, val, enrollmentId, User.Identity.Name, loggedUser.FirstName + " " + loggedUser.LastName);
            IList<ActivityViewModel> activityList = new List<ActivityViewModel>();
            if (!string.IsNullOrEmpty(enrollmentId))
            {
                activityList = this._enrollmentService.GetEnrollmentActivity(Convert.ToInt32(enrollmentId));
            }

            return this.Json(
                              new
                              {
                                  Success = true,
                                  Message = string.Empty,
                                  Html = this.RenderPartialViewToString("_OnboardActivity", activityList)
                              });
        }

        [HttpPost]
        public IActionResult UpdateClientTaskList(string id, string state, string type, string val, string tagHelper, string enrollmentId)
        {
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            this._checklistService.UpdateClientChecklistItem(Convert.ToInt32(id), state == "Y" ? true : false, type, val, enrollmentId, User.Identity.Name, loggedUser.FirstName + " " + loggedUser.LastName);

            IList<ActivityViewModel> activityList = new List<ActivityViewModel>();
            if (!string.IsNullOrEmpty(enrollmentId))
            {
                activityList = this._enrollmentService.GetEnrollmentActivity(Convert.ToInt32(enrollmentId));
            }

            return this.Json(
                              new
                              {
                                  Success = true,
                                  Message = string.Empty,
                                  Html = this.RenderPartialViewToString("_OnboardActivity", activityList)
                              });
        }

        [HttpPost]
        public IActionResult UpdateVendorTaskList(string id, string state, string type, string val, string tagHelper, string enrollmentId)
        {
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            this._checklistService.UpdateVendorChecklistItem(Convert.ToInt32(id), state == "Y" ? true : false, type, val, tagHelper, enrollmentId, User.Identity.Name, loggedUser.FirstName + " " + loggedUser.LastName);
            IList<ActivityViewModel> activityList = new List<ActivityViewModel>();
            if (!string.IsNullOrEmpty(enrollmentId))
            {
                activityList = this._enrollmentService.GetEnrollmentActivity(Convert.ToInt32(enrollmentId));
            }

            return this.Json(
                              new
                              {
                                  Success = true,
                                  Message = string.Empty,
                                  Html = this.RenderPartialViewToString("_OnboardActivity", activityList)
                              });
        }

        public IActionResult RefreshClientContacts(string clientId)
        {
            List<SelectListItem> contacts = this._lookupService.GetClientContacts(Convert.ToInt32(clientId));

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_CandiateClientContact", contacts)
                            });
        }

        public IActionResult Onboarding(int candidateId)
        {
            CandidateViewModel model = new CandidateViewModel();
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            model = this._candidateService.GetMyCandidates(loggedUser.Id, loggedUser.ProductOwnerId).Where(r => r.CandidateId == candidateId).FirstOrDefault();
            model.CommentsList = this._enrollmentService.GetEnrollmentComments(model.EnrollmentId);
            model.ActivityList = this._enrollmentService.GetEnrollmentActivity(model.EnrollmentId);
            model.Details = this._enrollmentService.GetEditCandidateDetails(model.EnrollmentId);
            model.EnrollmentCheckList = this._checklistService.GetEnrollmentCheklist(model.EnrollmentId, model.BillingType);
            model.TotalTasks = model.EnrollmentCheckList.Count();
            model.RemainingTasks = model.EnrollmentCheckList.Where(r => !r.IsChecked).Count();
            model.EnrollChecklistString = model.EnrollmentCheckList.Where(r => !r.IsChecked).Count() > 0 ? "panel-danger" : "panel-success";
            if (model.VendorId != 0)
            {
                model.VendorCheckList = this._checklistService.GetVendorCheklist(model.EnrollmentId, model.VendorId);
                model.TotalTasks += model.VendorCheckList.Count();
                model.RemainingTasks += model.VendorCheckList.Where(r => !r.IsChecked).Count();
                model.VendorChecklistString = model.VendorCheckList.Where(r => !r.IsChecked).Count() > 0 ? "panel-danger" : "panel-success";
            }
            else
            {
                model.VendorCheckList = new List<ChecklistViewModel>();
            }

            if (model.ClientId != 0)
            {
                model.ClientCheckList = this._checklistService.GetClientCheklist(model.EnrollmentId);
                model.TotalTasks += model.ClientCheckList.Count();
                model.RemainingTasks += model.ClientCheckList.Where(r => !r.IsChecked).Count();
                model.ClientChecklistString = model.ClientCheckList.Where(r => !r.IsChecked).Count() > 0 ? "panel-danger" : "panel-success";
            }
            else
            {
                model.ClientCheckList = new List<ChecklistViewModel>();
            }

            if (model.TotalTasks != 0)
            {
                model.PercentComplete = Convert.ToInt32( 
                    Math.Round(
                        (double)
                        ((decimal)(model.TotalTasks - model.RemainingTasks) / model.TotalTasks) * 100, 0));
            }
            else
            {
                model.PercentComplete = 0;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateCandidate(OnboardingDetails model)
        {
            if (model != null && ModelState.IsValid)
            {
                bool valid = true;
                if (!string.IsNullOrEmpty(model.BillRate) && !string.IsNullOrEmpty(model.PayRate) && Convert.ToDecimal(model.BillRate) < Convert.ToDecimal(model.PayRate))
                {
                    valid = false;
                    ModelState.AddModelError("PayRate", "Pay Rate can't exceed Bill Rate.");

                    
                }

                if (!string.IsNullOrEmpty(model.ContactEmail) && string.IsNullOrEmpty(model.ContactName.Trim()))
                {
                    valid = false;
                    ModelState.AddModelError("ContactFirstName", "Contact Name is required.");
                }

                if (!valid)
                {
                    return this.Json(
                              new
                              {
                                  Success = false,
                                  Message = "Validation failed",
                                  Html = this.RenderPartialViewToString("_OnboardDetails", model)
                              });
                }

                var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
                model.CurrentUser = loggedUser.UserName;

                this._enrollmentService.UpdateCandidateDetails(model);
                model = this._enrollmentService.GetEditCandidateDetails(model.EnrollmentId);

                return this.Json(
                              new
                              {
                                  Success = true,
                                  Message = string.Empty,
                                  Html = this.RenderPartialViewToString("_OnboardDetails", model)
                              });
            }
            else
            {
                return this.Json(
                              new
                              {
                                  Success = false,
                                  Message = "Validation failed",
                                  Html = this.RenderPartialViewToString("_OnboardDetails", model)
                              });
            }
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

        public IActionResult PrepareCandiateAdd(AddCandidateViewModel model)
        {
            if(model == null)
            {
                model = new AddCandidateViewModel();
            }
            
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();

            model.PortfolioManagers = this._userService.GetPortfolioManagers(loggedUser.ProductOwnerId);
            model.EndClients = this._lookupService.GetEndClients(loggedUser.ProductOwnerId);
            model.TaxStatuses = this._lookupService.GetTaxStatuses();
            model.Vendors = this._lookupService.GetVendors(loggedUser.ProductOwnerId);
            model.VisaTypes = this._lookupService.GetVisaTypes();
            model.Clients = this._lookupService.GetClients(loggedUser.ProductOwnerId);
            if (!string.IsNullOrEmpty(model.Client))
            {
                model.ClientContacts = this._lookupService.GetClientContacts(Convert.ToInt32(model.Client));
            }
            else
            {
                model.ClientContacts = this._lookupService.GetClientContacts(Convert.ToInt32(model.Clients.First().Value));
            }
            
            //viewModel.DurationMonths = ;
            //viewModel.DurationYears = ;

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_AddCandidate", model)
                            });
        }

        public IActionResult AddCandiate(AddCandidateViewModel model)
        {
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();

            if (model != null && ModelState.IsValid)
            {
                this._candidateService.AddCandidate(model, loggedUser.ProductOwnerId);
                IList<CandidateViewModel> pendingCandidates = this._candidateService.GetPendingCandidates(loggedUser.ProductOwnerId);
                IList<CandidateViewModel> assignedCandidates = this._candidateService.GetMyCandidates(loggedUser.Id, loggedUser.ProductOwnerId);
                return this.Json(
                           new
                           {
                               Success = true,
                               Message = "Saved Successfully.",
                               Html = this.RenderPartialViewToString("_PendingCandidates", pendingCandidates),
                               Count = pendingCandidates.Count.ToString(),
                               MyHtml = this.RenderPartialViewToString("_PendingCandidates", assignedCandidates),
                               MyCount = assignedCandidates.Count.ToString(),
                           });
            }

            if (model == null)
            {
                model = new AddCandidateViewModel();
            }

            model.PortfolioManagers = this._userService.GetPortfolioManagers(loggedUser.ProductOwnerId);
            model.EndClients = this._lookupService.GetEndClients(loggedUser.ProductOwnerId);
            model.TaxStatuses = this._lookupService.GetTaxStatuses();
            model.Vendors = this._lookupService.GetVendors(loggedUser.ProductOwnerId);
            model.VisaTypes = this._lookupService.GetVisaTypes();
            model.Clients = this._lookupService.GetClients(loggedUser.ProductOwnerId);
            if (!string.IsNullOrEmpty(model.Client))
            {
                model.ClientContacts = this._lookupService.GetClientContacts(Convert.ToInt32(model.Client));
            }
            else
            {
                model.ClientContacts = this._lookupService.GetClientContacts(Convert.ToInt32(model.Clients.First().Value));
            }

            //viewModel.DurationMonths = ;
            //viewModel.DurationYears = ;

            return this.Json(
                            new
                            {
                                Success = false,
                                Message = "Validation Failed",
                                Html = this.RenderPartialViewToString("_AddCandidate", model)
                            });
        }

        public IActionResult PrepareEnrollmentAssign(string enrollmentId)
        {
            AssignEnrollemntViewModel model = new AssignEnrollemntViewModel();
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            model.EnrollmentId = enrollmentId;
            model.HRUsers = this._userService.GetHRUsers(loggedUser.ProductOwnerId);

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

                    // Update Grid
                    IList<CandidateViewModel> pendingCandidates = this._candidateService.GetPendingCandidates(loggedUser.ProductOwnerId);
                    //mygrid
                    IList<CandidateViewModel> assignedCandidates = this._candidateService.GetMyCandidates(loggedUser.Id, loggedUser.ProductOwnerId);
                    
                    return this.Json(
                               new
                               {
                                   Success = true,
                                   Message = "Saved Successfully.",
                                   Html = this.RenderPartialViewToString("_PendingCandidates", pendingCandidates),
                                   Count = pendingCandidates.Count.ToString(),
                                   MyHtml = this.RenderPartialViewToString("_AssignedCandidates", assignedCandidates),
                                   MyCount = assignedCandidates.Count.ToString()
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

        public IActionResult EnrollmentAccept(string enrollmentId)
        {
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            if (!string.IsNullOrEmpty(enrollmentId))
            {
                try
                {
                    this._candidateService.AssignEnrollment(Convert.ToInt32(enrollmentId), loggedUser.Id, User.Identity.Name);

                    // Update Grid
                    IList<CandidateViewModel> pendingCandidates = this._candidateService.GetPendingCandidates(loggedUser.ProductOwnerId);
                    //mygrid
                    IList<CandidateViewModel> assignedCandidates = this._candidateService.GetMyCandidates(loggedUser.Id, loggedUser.ProductOwnerId);
                    return this.Json(
                               new
                               {
                                   Success = true,
                                   Message = "Saved Successfully.",
                                   Html = this.RenderPartialViewToString("_PendingCandidates", pendingCandidates),
                                   Count = pendingCandidates.Count.ToString(),
                                   MyHtml = this.RenderPartialViewToString("_AssignedCandidates", assignedCandidates),
                                   MyCount = assignedCandidates.Count.ToString()
                               });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("HRUser", e.Message);
                }

                AssignEnrollemntViewModel model = new AssignEnrollemntViewModel();

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

        public IActionResult PrepareAddEndClient(int clientId)
        {
            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_AddEndClient", new EndClientsViewModel())
                            });
        }

        [HttpPost]
        public IActionResult AddEndClient(EndClientsViewModel client)
        {
            if (client != null && ModelState.IsValid)
            {
                var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
                client.CurrentUser = loggedUser.UserName;
                bool success = this._databaseService.AddEndClient(loggedUser.ProductOwnerId, client);

                if (!success)
                {
                    ModelState.AddModelError("CompanyName", "Client Name already exists.");
                    return this.GetClientViewResult(client);
                }
                else
                {
                    IList<EndClientsViewModel> model = this._databaseService.GetEndClients(loggedUser.ProductOwnerId);
                    List<SelectListItem> endClients = this._lookupService.GetEndClients(loggedUser.ProductOwnerId);
                    return this.Json(
                                    new
                                    {
                                        Success = true,
                                        Message = "Saved Successfully",
                                        Html = this.RenderPartialViewToString("_CandiateEndClient", endClients),
                                        Count = model.Count
                                    });
                }
            }
            else
            {
                return this.GetClientViewResult(client);
            }
        }

        public IActionResult PrepareAddClientContact(int clientId)
        {
            ClientContactViewModel viewModel = new ClientContactViewModel();
            viewModel.ClientId = clientId;
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            SelectListItem theClient = this._lookupService.GetClients(loggedUser.ProductOwnerId).Where(r => r.Value == clientId.ToString()).First();
            viewModel.ClientName = theClient.Text;

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_AddClientContact", viewModel)
                            });
        }

        [HttpPost]
        public IActionResult AddClientContact(ClientContactViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
                model.CurrentUser = loggedUser.UserName;
                bool isValid = this._databaseService.AddClientContact(model);

                if (isValid)
                {
                    List<SelectListItem> contacts = this._lookupService.GetClientContacts(model.ClientId);
                    return this.Json(
                                    new
                                    {
                                        Success = true,
                                        Message = "Saved Successfully",
                                        Html = this.RenderPartialViewToString("_CandiateClientContact", contacts)
                                    });
                }
                else
                {
                    ModelState.AddModelError("Name", "Contact with same name already exists.");
                    return this.Json(
                            new
                            {
                                Success = false,
                                Message = "Validation failed",
                                Html = this.RenderPartialViewToString("_AddClientContact", model)
                            });
                }
            }
            else
            {
                return this.Json(
                            new
                            {
                                Success = false,
                                Message = "Validation failed",
                                Html = this.RenderPartialViewToString("_AddClientContact", model)
                            });
            }
        }

        public IActionResult PrepareAddComment(int enrollmentId)
        {
            CommentViewModel viewModel = new CommentViewModel();

            viewModel.CommentEnrollmentId = enrollmentId;

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_AddComment", viewModel)
                            });
        }

        [HttpPost]
        public IActionResult AddComment(string CommentEnrollmentId, string Comment)
        {
            CommentViewModel comment = new CommentViewModel();
            comment.CommentEnrollmentId = Convert.ToInt32(CommentEnrollmentId);
            comment.Comment = Comment;

            if (comment != null && ModelState.IsValid)
            {
                var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
                comment.CurrentUser = loggedUser.UserName;
                this._enrollmentService.AddComment(comment);

                IList<CommentsViewModel> model = this._enrollmentService.GetEnrollmentComments(comment.CommentEnrollmentId);
                return this.Json(
                                new
                                {
                                    Success = true,
                                    Message = "Saved Successfully",
                                    Html = this.RenderPartialViewToString("_OnboardComments", model),
                                });
            }
            else
            {
                return this.GetViewResult(comment);
            }
        }

        public ActionResult AddVendor(VendorsViewModel Vendor)
        {
            if (Vendor != null && ModelState.IsValid)
            {
                var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
                Vendor.CurrentUser = loggedUser.UserName;
                bool success = this._databaseService.AddVendor(loggedUser.ProductOwnerId, Vendor);

                if (!success)
                {
                    ModelState.AddModelError("CompanyName", "Vendor Name already exists.");
                    return this.GetVendorViewResult(Vendor);
                }
                else
                {
                    List<SelectListItem> model = this._lookupService.GetVendors(loggedUser.ProductOwnerId);
                    return this.Json(
                                    new
                                    {
                                        Success = true,
                                        Message = "Saved Successfully",
                                        Html = this.RenderPartialViewToString("_CandiateVendor", model)
                                    });
                }
            }
            else
            {
                return this.GetVendorViewResult(Vendor);
            }
        }

        public IActionResult OnboardCandidate(string enrollmentId)
        {
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            if (!string.IsNullOrEmpty(enrollmentId))
            {
                try
                {
                    this._candidateService.OnboardEnrollment(Convert.ToInt32(enrollmentId), loggedUser.Id, User.Identity.Name);

                    // Update Grid
                    IList<CandidateViewModel> pendingCandidates = this._candidateService.GetPendingCandidates(loggedUser.ProductOwnerId);

                    return this.Json(
                            new
                            {
                                Success = true,
                                Message = "Saved Successfully.",
                                Html = string.Empty
                            });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("HRUser", e.Message);
                    return this.GetJsonResult(false, "Validation failed", this.ModelErrors());
                }
            }
            else
            {
                List<string> error = new List<string>();
                error.Add("Enrollment Id can't be empty");
                return this.GetJsonResult(false, "Validation failed", error);
            }
        }

        private JsonResult GetViewResult(CommentViewModel comment)
        {
            return this.Json(
                            new
                            {
                                Success = false,
                                Message = "Validation failed",
                                Html = this.RenderPartialViewToString("_AddComment", comment)
                            });
        }

        private JsonResult GetVendorViewResult(VendorsViewModel vendor)
        {
            return this.Json(
                            new
                            {
                                Success = false,
                                Message = "Validation failed",
                                Html = this.RenderPartialViewToString("_AddVendor", vendor)
                            });
        }

        private JsonResult GetClientViewResult(EndClientsViewModel client)
        {
            //user = this.AppendModelCombos(user);

            return this.Json(
                            new
                            {
                                Success = false,
                                Message = "Validation failed",
                                Html = this.RenderPartialViewToString("EnClient", client)
                            });
        }
    }
}