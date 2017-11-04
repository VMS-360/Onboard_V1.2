using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Onboard.Web.UI.Controllers
{
    public class ExecutiveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}