using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onboard.Web.UI.Models.HRViewModels
{
    public class CandidateSpecViewModel
    {
        public List<List<string>> Onboarded { get; set; }

        public List<List<string>> Pending { get; set; }
    }
}
