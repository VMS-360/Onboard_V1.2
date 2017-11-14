using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Onboard.Web.UI.Models;
using Onboard.Web.UI.Models.DatabaseViewModels;
using Onboard.Web.UI.Models.HRViewModels;
using Onboard.Web.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Onboard.Web.UI.Controllers
{
    public class DatabaseController : OnboardController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDatabaseService _databaseService;
        private readonly ICandidateService _candidateService;
        private readonly ICompositeViewEngine _viewEngine;

        public DatabaseController(IDatabaseService databaseService,
                                       ICandidateService candidateService,
                                       UserManager<ApplicationUser> userManager,
                                       ICompositeViewEngine viewEngine) : base(viewEngine)
        {
            this._candidateService = candidateService;
            this._databaseService = databaseService;
            this._userManager = userManager;
        }



        public IActionResult Clients()
        {
            IList<ClientsViewModel> model = new List<ClientsViewModel>();
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            model = this._databaseService.GetClients(loggedUser.ProductOwnerId);

            return View(model);
        }

        public IActionResult ClientSearch(string searchString)
        {
            IList<ClientsViewModel> model = new List<ClientsViewModel>();
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            model = this._databaseService.GetClients(loggedUser.ProductOwnerId);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(r => !string.IsNullOrEmpty(r.CompanyName) && r.CompanyName.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_ClientList", model)
                            });
        }

        public IActionResult ClientDetails(string clientId)
        {
            ClientsViewModel model = new ClientsViewModel();
            if (!string.IsNullOrEmpty(clientId))
            {
                model = this._databaseService.GetClientDetails(Convert.ToInt32(clientId));
            }

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_ClientDetails", model)
                            });
        }

        public IActionResult PrepareAddClient()
        {
            ClientsViewModel viewModel = new ClientsViewModel();

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_AddClient", viewModel)
                            });
        }

        public ActionResult AddClient(ClientsViewModel client)
        {
            if (client != null && ModelState.IsValid)
            {
                var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
                client.CurrentUser = loggedUser.UserName;
                bool success = this._databaseService.AddClient(loggedUser.ProductOwnerId, client);

                if (!success)
                {
                    ModelState.AddModelError("CompanyName", "Client Name already exists.");
                    return this.GetViewResult(client);
                }
                else
                {
                    IList<ClientsViewModel> model = this._databaseService.GetClients(loggedUser.ProductOwnerId);
                    return this.Json(
                                    new
                                    {
                                        Success = true,
                                        Message = "Saved Successfully",
                                        Html = this.RenderPartialViewToString("_ClientList", model),
                                        Count = model.Count
                                    });
                }
            }
            else
            {
                return this.GetViewResult(client);
            }
        }

        public IActionResult Vendors()
        {
            IList<VendorsViewModel> model = new List<VendorsViewModel>();
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            model = this._databaseService.GetVendors(loggedUser.ProductOwnerId);

            return View(model);
        }

        public IActionResult VendorSearch(string searchString)
        {
            IList<VendorsViewModel> model = new List<VendorsViewModel>();
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            model = this._databaseService.GetVendors(loggedUser.ProductOwnerId);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(r => !string.IsNullOrEmpty(r.CompanyName) && r.CompanyName.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_VendorList", model)
                            });
        }

        public IActionResult VendorDetails(string VendorId)
        {
            VendorsViewModel model = new VendorsViewModel();
            if (!string.IsNullOrEmpty(VendorId))
            {
                model = this._databaseService.GetVendorDetails(Convert.ToInt32(VendorId));
            }

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_VendorDetails", model)
                            });
        }

        public IActionResult PrepareAddVendor()
        {
            VendorsViewModel viewModel = new VendorsViewModel();

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_AddVendor", viewModel)
                            });
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
                    IList<VendorsViewModel> model = this._databaseService.GetVendors(loggedUser.ProductOwnerId);
                    return this.Json(
                                    new
                                    {
                                        Success = true,
                                        Message = "Saved Successfully",
                                        Html = this.RenderPartialViewToString("_VendorList", model),
                                        Count = model.Count
                                    });
                }
            }
            else
            {
                return this.GetVendorViewResult(Vendor);
            }
        }

        public IActionResult EndClients()
        {
            IList<EndClientsViewModel> model = new List<EndClientsViewModel>();
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            model = this._databaseService.GetEndClients(loggedUser.ProductOwnerId);

            return View(model);

        }

        public IActionResult EndClientSearch(string searchString)
        {
            IList<EndClientsViewModel> model = new List<EndClientsViewModel>();
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            model = this._databaseService.GetEndClients(loggedUser.ProductOwnerId);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(r => !string.IsNullOrEmpty(r.CompanyName) && r.CompanyName.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("AddEndClient", model)
                            });
        }

        public IActionResult EndClientDetails(string EndclientId)
        {
            EndClientsViewModel model = new EndClientsViewModel();
            if (!string.IsNullOrEmpty(EndclientId))
            {
                model = this._databaseService.GetEndClientDetails(Convert.ToInt32(EndclientId));
            }

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("EndClientDetails", model)
                            });
        }

        public IActionResult PrepareAddEndClient()
        {
            EndClientsViewModel viewModel = new EndClientsViewModel();

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("AddEndClient", viewModel)
                            });
        }

        public ActionResult AddEndClient(EndClientsViewModel client)
        {
            if (client != null && ModelState.IsValid)
            {
                var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
                client.CurrentUser = loggedUser.UserName;
                bool success = this._databaseService.AddEndClient(loggedUser.ProductOwnerId, client);

                if (!success)
                {
                    ModelState.AddModelError("CompanyName", "Client Name already exists.");
                    return this.GetViewResult(client);
                }
                else
                {
                    IList<EndClientsViewModel> model = this._databaseService.GetEndClients(loggedUser.ProductOwnerId);
                    return this.Json(
                                    new
                                    {
                                        Success = true,
                                        Message = "Saved Successfully",
                                        Html = this.RenderPartialViewToString("EndClientList", model),
                                        Count = model.Count
                                    });
                }
            }
            else
            {
                return this.GetViewResult(client);
            }
        }

        public IActionResult Consultants()
        {
            IList<CandidateViewModel> model = new List<CandidateViewModel>();
            var loggedUser = this._userManager.Users.Where(r => r.UserName == User.Identity.Name).FirstOrDefault();
            model = this._candidateService.GetAllOnboardedCandidates(loggedUser.ProductOwnerId, 0);

            return View(model);
        }

        public IActionResult ConsultantDetails(string EnrollmentId)
        {
            CandidateViewModel model = new CandidateViewModel();
            if (!string.IsNullOrEmpty(EnrollmentId))
            {
                model = this._candidateService.GetConsultantDetails(Convert.ToInt32(EnrollmentId));
            }
            return this.Json(
                           new
                           {
                               Success = true,
                               Message = string.Empty,
                               Html = this.RenderPartialViewToString("ConsultantDetails", model)
                           });

        }

        private JsonResult GetViewResult(ClientsViewModel client)
        {
            //user = this.AppendModelCombos(user);

            return this.Json(
                            new
                            {
                                Success = false,
                                Message = "Validation failed",
                                Html = this.RenderPartialViewToString("_AddClient", client)
                            });
        }

        private JsonResult GetVendorViewResult(VendorsViewModel vendor)
        {
            //user = this.AppendModelCombos(user);

            return this.Json(
                            new
                            {
                                Success = false,
                                Message = "Validation failed",
                                Html = this.RenderPartialViewToString("_AddVendor", vendor)
                            });
        }

        private JsonResult GetViewResult(EndClientsViewModel client)
        {
            //user = this.AppendModelCombos(user);

            return this.Json(
                            new
                            {
                                Success = false,
                                Message = "Validation failed",
                                Html = this.RenderPartialViewToString("AddEndClient", client)
                            });
        }

    }
}