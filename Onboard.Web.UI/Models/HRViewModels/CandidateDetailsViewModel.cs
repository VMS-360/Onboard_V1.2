using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onboard.Web.UI.Models.HRViewModels
{
    public class CandidateDetailsViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ClientName { get; set; }

        public string BillingType { get; set; }

        public string StatusIndicator { get; set; }

        public string AccountManager { get; internal set; }

        public string VendorName { get; set; }

        public string EndClient { get; set; }

        public string ClientContact { get; set; }

        public DateTime CreatedDate { get; set; }

        public string DateCreatedString
        {
            get
            {
                return String.Format("{0:MM.dd.yy h:mm tt}", this.CreatedDate);
            }
        }
    }
}
