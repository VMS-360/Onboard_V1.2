using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onboard.Web.UI.Models.HRViewModels
{
    public class ManagementViewModel
    {
        public string ActiveTab { get; set; }

        public IList<CandidateViewModel> PendingCandidates { get; set; }

        public IList<CandidateViewModel> AssignedCandidates { get; set; }

        public IList<CandidateViewModel> RecentOnboards { get; set; }
    }
}
