using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onboard.Web.UI.Models.HomeViewModels
{
    public class DashboardViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UnassignedCount { get; set; }

        public int PendingCount { get; set; }
    }
}
