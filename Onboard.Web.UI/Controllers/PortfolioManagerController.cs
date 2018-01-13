using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Onboard.Web.UI.Controllers
{
    public class PortfolioManagerController : Controller
    {
        [Authorize(Roles = "Admin, Portfolio")]
        public IActionResult Index()
        {
            return View();
        }
    }
}