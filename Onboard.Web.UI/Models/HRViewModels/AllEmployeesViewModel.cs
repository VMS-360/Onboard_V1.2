using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onboard.Web.UI.Models.HRViewModels
{
    public class AllEmployeesViewModel
    {
        public int EnrollmentId { get; set; }
        public string EmployeeName { get; set; }
        public string CandidateName { get; set; }
        public string ClietName { get; set; }
        public string TaxStatus { get; set; }
        public DateTime LastUpdated { get; set; }
        public string LastUpdatedString
        {
            get
            {
                return string.Format("Created {0}.{1}.{2} ", this.LastUpdated.Month, this.LastUpdated.Day, this.LastUpdated.Year.ToString().Length == 4 ? this.LastUpdated.Year.ToString().Substring(2, 2) : "1");
            }
        }
    }
}