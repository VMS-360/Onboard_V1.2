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
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;

namespace Onboard.Web.UI.Controllers
{
    public class AccountManagerController : OnboardController
    {
        private readonly ICandidateService _candidateService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly IUserLookupService _userService;
        private readonly ILookupService _lookupService;
        private readonly IEmailSender _emailSender;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IChecklistService _checklistService;
        private readonly IDatabaseService _databaseService;


        public AccountManagerController(ICandidateService candidateService,
                                       UserManager<ApplicationUser> userManager,
                                       ICompositeViewEngine viewEngine,
                                       IUserLookupService userService,
                                       ILookupService lookupService,
                                       IEmailSender sendEmailService,
                                       IEnrollmentService enrollmentService,
                                       IChecklistService checklistService,
                                       IDatabaseService databaseService) : base(viewEngine)

        {
            this._candidateService = candidateService;
            this._userManager = userManager;
            this._userService = userService;
            this._lookupService = lookupService;
            this._emailSender = sendEmailService;
            this._enrollmentService = enrollmentService;
            this._checklistService = checklistService;
            this._databaseService = databaseService;
        }

        [Authorize(Roles = "Admin, Account Manager")]
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
        
        
        //**** included *****

        public IActionResult PrepareCandiateAdd(AddCandidateViewModel model)
        {
            if (model == null)
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
            model.States = this._lookupService.GetStates();
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
         

            MailMessage msg = new MailMessage();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            
                msg.Subject = "OnBoard Application : New Candidate";
                msg.Body = "<html><body><div style='border: 0.5px solid black;padding:10px 100px 10px 10px'><h1>New Candidate has been created for paperwork, please login to portal to start paperwork <a href='http://3sixty.us'> Login Here! </a> </h1> </div></body></html>";
                msg.From = new MailAddress("onboard@themesoft.com");
                msg.To.Add("meena@themesoft.com");
                msg.IsBodyHtml = true;
                client.Host = "smtp.office365.com";
                System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("onboard@themesoft.com", "theme@123");
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
           
           
            //this._emailSender.SendEmailAsync("pradil90@gmail.com", "ff", "bhyhyyh");
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
            model.States = this._lookupService.GetStates();
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
    }
}