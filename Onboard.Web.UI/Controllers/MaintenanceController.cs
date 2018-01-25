using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Onboard.Web.UI.Models;
using Onboard.Web.UI.Models.MaintenanceViewModels;
using Onboard.Web.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onboard.Web.UI.Controllers
{
    public class MaintenanceController : OnboardController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ICompositeViewEngine _viewEngine;

        public MaintenanceController(UserManager<ApplicationUser> userManager, 
                                    RoleManager<ApplicationRole> roleManager,
                                    ICompositeViewEngine viewEngine) : base(viewEngine)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            UserListViewModel model = new UserListViewModel();
            IList<ApplicationRole> roles = this._roleManager.Roles.OrderBy(r => r.Name).ToList();
            var theRoles = roles.Select(c => new OnboardRoleViewModel
            {
                RoleId = c.Id,
                RoleName = c.Name
            })
                        .OrderBy(e => e.RoleName);

            ViewData["roles"] = theRoles;
            ViewData["defaultRole"] = theRoles.First();
            model.UserCount = this._userManager.Users.Count();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users1()
        {
            UserListViewModel model = new UserListViewModel();
            IList<ApplicationRole> roles = this._roleManager.Roles.OrderBy(r => r.Name).ToList();
            ViewData["AllRoles"] = roles.Select(r => new OnboardRoleViewModel
            {
                RoleId = r.Id,
                RoleName = r.Name
            }).ToList();
            ViewData["defaultRole"] = roles.First();
            model.UserCount = this._userManager.Users.Count();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users2()
        {
            UserListViewModel model = new UserListViewModel();
            IList<ApplicationRole> roles = this._roleManager.Roles.OrderBy(r => r.Name).ToList();
            ViewData["AllRoles"] = roles.Select(r => new OnboardRoleViewModel
            {
                RoleId = r.Id,
                RoleName = r.Name
            }).ToList();
            ViewData["defaultRole"] = roles.First();
            model.UserCount = this._userManager.Users.Count();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users3()
        {
            UserListViewModel model = new UserListViewModel();
            IList<ApplicationRole> roles = this._roleManager.Roles.OrderBy(r => r.Name).ToList();
            ViewData["AllRoles"] = roles.Select(r => new OnboardRoleViewModel
            {
                RoleId = r.Id,
                RoleName = r.Name
            }).ToList();
            ViewData["defaultRole"] = roles.First();
            model.UserCount = this._userManager.Users.Count();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users4()
        {
            UserListViewModel model = new UserListViewModel();
            IList<ApplicationRole> roles = this._roleManager.Roles.OrderBy(r => r.Name).ToList();
            var theRoles = roles.Select(c => new OnboardRoleViewModel
                        {
                            RoleId = c.Id,
                            RoleName = c.Name
                        })
                        .OrderBy(e => e.RoleName);

            ViewData["roles"] = theRoles;
            ViewData["defaultRole"] = theRoles.First();
            model.UserCount = this._userManager.Users.Count();

            return View(model);
        }

        public async Task<ActionResult> GetUsers([DataSourceRequest] DataSourceRequest request)
        {
            List<OnboardUserViewModel> Users = await this.GetCurrentUsers();

            return Json(Users.ToDataSourceResult(request));
        }

        public async Task<IActionResult> PrepareUserDetails(string userId)
        {
            OnboardUserViewModel viewModel = new OnboardUserViewModel();
            if (userId != null && userId.Length > 0)
            {
                List<OnboardUserViewModel> Users = await this.GetCurrentUsers(Convert.ToInt32(userId));
                viewModel = Users.FirstOrDefault();
            }

            viewModel = this.AppendModelCombos(viewModel);

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_UserDetails", viewModel)
                            });
        }

        public async Task<IActionResult> PrepareAddUser(string userId)
        {
            OnboardUserViewModel viewModel = new OnboardUserViewModel();
            if (userId!=null && userId .Length >0)
            {
                List<OnboardUserViewModel> Users = await this.GetCurrentUsers(Convert.ToInt32(userId));
                viewModel = Users.FirstOrDefault();
            }

            viewModel = this.AppendModelCombos(viewModel);

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = string.Empty,
                                Html = this.RenderPartialViewToString("_AddUser", viewModel)
                            });
        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> DeleteUsers(string users)
        {
            if (users != null && users.Length > 0)
            {
                string[] usersList = users.Split('-');

                for(int i =0; i < usersList.Count() - 1; i++)
                {
                    var theUser = await this._userManager.FindByNameAsync(usersList[i]);
                    var result = await this._userManager.DeleteAsync(theUser);
                    if (!result.Succeeded)
                    {
                        throw new AccessViolationException("Delete user " + usersList[i] +
                                                                   " from application database failed with error(s): " + result.Errors);
                    }
                }
            }

            return this.Json(
                            new
                            {
                                Success = true,
                                Message = "Deleted Users",
                                Html = this._userManager.Users.Count()
                            });
        }

        public async Task<ActionResult> AddUser(OnboardUserViewModel user)
        {
            if (user != null && ModelState.IsValid)
            {
                var theUser = await this._userManager.FindByNameAsync(user.UserName);
                var currentUser = await this._userManager.FindByNameAsync(User.Identity.Name);
                if (theUser == null)
                {
                    var newUser = new ApplicationUser();
                    newUser.UserName = user.UserName;
                    newUser.FirstName = user.FirstName;
                    newUser.LastName = user.LastName;
                    newUser.IsInternal = user.IsInternal ? "Y" : "N";
                    newUser.Email = user.Email;
                    newUser.ProductOwnerId = currentUser.ProductOwnerId;

                    //Look for duplicate user email.
                    var dupUser = this._userManager.Users.Where(r => r.Email.ToLower() == user.Email.ToLower()).FirstOrDefault();
                    if(dupUser != null)
                    {
                        ModelState.AddModelError("Email", "An user with the same email " + user.Email +
                                                       " already exists.");
                        return this.GetViewResult(user);
                    }

                    var userResult = await this._userManager.CreateAsync(newUser);
                    theUser = await this._userManager.FindByNameAsync(user.UserName);

                    if (!userResult.Succeeded)
                    {
                        ModelState.AddModelError("UserName", "Adding user " + user.UserName +
                                                       " failed with error(s): " + userResult.Errors);
                        return this.GetViewResult(user);
                    }
                    else
                    {
                        //Set password 
                        if (user.Password == null || user.Password.Length == 0)
                        {
                            user.Password = "Abc@123";
                        }

                        userResult = await this._userManager.AddPasswordAsync(theUser, user.Password);

                        if (!userResult.Succeeded)
                        {
                            ModelState.AddModelError("Password", "Adding Password for user " + user.UserName +
                                                           " failed with error(s): " + userResult.Errors);
                            return this.GetViewResult(user);
                        }
                    }

                    try
                    {
                        var roleName = this._roleManager.Roles.Where(r => r.Id == Convert.ToInt32(user.RoleId)).First().Name;

                        var roleResult = await this._userManager.AddToRoleAsync(theUser, roleName);
                        if (!userResult.Succeeded)
                        {
                            if (!roleResult.Succeeded)
                            {
                                ModelState.AddModelError("RoleId", "Creating role for  user " + user.UserName + "failed with error(s): " + roleResult.Errors);
                                return this.GetViewResult(user);
                            }
                        }
                    }
                    catch
                    {
                        ModelState.AddModelError("RoleId", "Role not found");
                        return this.GetViewResult(user);
                    }

                    return this.GetJsonResult(true, "Saved Successfully", null);
                }
                else
                {
                    ModelState.AddModelError("UserName", "UserName already exists.");
                    return this.GetViewResult(user);
                }
            }
            else
            {
                return this.GetJsonResult(false, "Validation failed", this.ModelErrors());
            }
        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> UpdateSelectedUser(OnboardUserViewModel user)
        {
            if (user != null && ModelState.IsValid)
            {
                var theUser = await this._userManager.FindByNameAsync(user.UserName);
                var userRoles = await this._userManager.GetRolesAsync(theUser);

                theUser.FirstName = user.FirstName;
                theUser.LastName = user.LastName;
                theUser.IsInternal = user.IsInternal ? "Y" : "N";
                theUser.Email = user.Email;

                await this._userManager.UpdateAsync(theUser);
                var roleName = this._roleManager.Roles.Where(r => r.Id == Convert.ToInt32(user.RoleId)).First().Name;
                if (userRoles != null && userRoles.Count > 0 && userRoles.First() != roleName)
                {
                    await this._userManager.RemoveFromRoleAsync(theUser, userRoles.First());
                    await this._userManager.AddToRoleAsync(theUser, roleName);
                }
            }

            return this.GetJsonResult(true, "Saved Successfully", null);
        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> UpdateUser([DataSourceRequest] DataSourceRequest request,
             OnboardUserViewModel user)
        {
            if (user != null && ModelState.IsValid)
            {
                var theUser = await this._userManager.FindByNameAsync(user.UserName);
                var userRoles = await this._userManager.GetRolesAsync(theUser);

                theUser.FirstName = user.FirstName;
                theUser.LastName = user.LastName;
                theUser.IsInternal = user.IsInternal ? "Y" : "N";
                theUser.Email = user.Email;

                await this._userManager.UpdateAsync(theUser);

                if (userRoles != null && userRoles.Count > 0 && userRoles.First() != user.Role.RoleName)
                {
                    await this._userManager.RemoveFromRoleAsync(theUser, userRoles.First());
                    await this._userManager.AddToRoleAsync(theUser, user.Role.RoleName);
                }
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> UpdateUsers([DataSourceRequest] DataSourceRequest request,
             [Bind(Prefix = "models")]IEnumerable<OnboardUserViewModel> users)
        {
            if(users != null)
            {
                foreach (var user in users)
                {
                    if (user != null && ModelState.IsValid)
                    {
                        var theUser = await this._userManager.FindByNameAsync(user.UserName);
                        var userRoles = await this._userManager.GetRolesAsync(theUser);

                        theUser.FirstName = user.FirstName;
                        theUser.LastName = user.LastName;
                        theUser.IsInternal = user.IsInternal ? "Y" : "N";
                        theUser.Email = user.Email;

                        await this._userManager.UpdateAsync(theUser);

                        if (userRoles != null && userRoles.Count > 0 && userRoles.First() != user.Role.RoleName)
                        {
                            await this._userManager.RemoveFromRoleAsync(theUser, userRoles.First());
                            await this._userManager.AddToRoleAsync(theUser, user.Role.RoleName);
                        }
                    }
                }
            }

            return Json(users.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> RemoveUser([DataSourceRequest] DataSourceRequest request, OnboardUserViewModel user)
        {
            if (user != null)
            {
                var theUser = await this._userManager.FindByNameAsync(user.UserName);
                var result = await this._userManager.DeleteAsync(theUser);
                if (!result.Succeeded)
                {
                    throw new AccessViolationException("Delete user " + user.UserName +
                                                               " from application database failed with error(s): " + result.Errors);
                }
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult GetRoles([DataSourceRequest] DataSourceRequest request)
        {
            var roles = this._roleManager
                            .Roles
                            .OrderBy(r => r.Name)
                            .Select(r => new OnboardRoleViewModel { RoleName = r.Name, RoleId = r.Id })
                            .ToList();

            return Json(roles);
        }
        
        private OnboardUserViewModel AppendModelCombos(OnboardUserViewModel viewModel)
        {
            viewModel.Roles = this._roleManager
                            .Roles
                            .OrderBy(r => r.Name)
                            .Select(r => new SelectListItem { Text = r.Name, Value = r.Id.ToString() })
                            .ToList();
            viewModel.Roles.Insert(0, new SelectListItem { Text = "", Value = "" });
            viewModel.Internals = new List<SelectListItem> { new SelectListItem { Text = "", Value = "" },
                new SelectListItem { Text = "Internal User", Value = "I" },
                new SelectListItem { Text = "External User", Value = "E" } };

            return viewModel;
        }

        private JsonResult GetViewResult(OnboardUserViewModel user)
        {
            user = this.AppendModelCombos(user);

            return this.Json(
                            new
                            {
                                Success = false,
                                Message = "Validation failed",
                                Html = this.RenderPartialViewToString("_AddUser", user)
                            });
        }

        private async Task<List<OnboardUserViewModel>> GetCurrentUsers(int userId = 0)
        {
            IList<ApplicationUser> users = this._userManager.Users.Where(r => userId == 0 || r.Id == userId) .OrderBy(r => r.FirstName).ThenBy(r => r.LastName).ToList();
            IList<ApplicationRole> roles = this._roleManager.Roles.OrderBy(r => r.Name).ToList();

            List<OnboardUserViewModel> Users = new List<OnboardUserViewModel>();
            foreach (ApplicationUser user in users)
            {
                var userRoles = await this._userManager.GetRolesAsync(user);
                var role = roles.Where(r => userRoles.Contains(r.Name)).Select(r => new OnboardRoleViewModel
                {
                    RoleId = r.Id,
                    RoleName = r.Name
                }).FirstOrDefault();

                Users.Add(new OnboardUserViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsInternal = user.IsInternal == "Y",
                    Internal = user.IsInternal == "Y"? "I" : "E",
                    UserName = user.UserName,
                    UserId = user.Id,
                    Role = role,
                    RoleId = role.RoleId,
                    RoleName = role.RoleName
                });
            }

            return Users;
        }
    }
}