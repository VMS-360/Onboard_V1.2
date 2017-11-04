using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Onboard.Web.UI.Models.HRViewModels
{
    public class CandidateViewModel
    {
        public int EnrollmentId { get; set; }

        public int CandidateId { get; set; }

        [DataType("Text")]
        [DisplayName("Candidate Name")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ClientName { get; set; }

        public string BillingType { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public int TotalTasks { get; set; }

        public int RemainingTasks { get; set; }

        public int PercentComplete { get; set; }

        public string VendorName { get; set; }

        public string CandidateName
        {
            get
            {
                return string.Format("{0} {1} for {2} on {3}", this.FirstName, this.LastName, this.ClientName, this.BillingType);
            }
        }

        public string CandidateVendor
        {
            get
            {
                return string.Format("{0} {1} for {2}", this.FirstName, this.LastName, this.ClientName);
            }
        }

        public string DateString
        {
            get
            {
                return string.Format("Created {0}.{1}.{2} ", this.CreatedDate.Month, this.CreatedDate.Day, this.CreatedDate.Year.ToString().Length == 4 ? this.CreatedDate.Year.ToString().Substring(2, 2) : "1");
            }
        }

        public string Created
        {
            get
            {
                return string.Format("{0}.{1}.{2} ", this.CreatedDate.Month, this.CreatedDate.Day, this.CreatedDate.Year.ToString().Length == 4 ? this.CreatedDate.Year.ToString().Substring(2, 2) : "1");
            }
        }

        public string Updated
        {
            get
            {
                if (this.ModifiedDate == new DateTime())
                {
                    return string.Empty;
                }
                else
                {
                    return string.Format("{0}.{1}.{2} ", this.ModifiedDate.Month, this.ModifiedDate.Day, this.ModifiedDate.Year.ToString().Length == 4 ? this.ModifiedDate.Year.ToString().Substring(2, 2) : "1");
                }
            }
        }

        public string UpdateString
        {
            get
            {
                if (this.ModifiedDate == new DateTime())
                {
                    return string.Empty;
                }
                else
                {
                    TimeSpan span = (DateTime.Now - ModifiedDate);

                    return string.Format("Last Updated : {0}h {1}m ago", span.Hours, span.Minutes);
                }
            }
        }

        public string VendorString
        {
            get
            {
                if (this.VendorName != null && this.VendorName.Length !=0)
                {
                    return string.Format("Vendor: {0}", this.VendorName);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string TimeEllapsed
        {
            get
            {
                TimeSpan span = (DateTime.Now - CreatedDate);

                return string.Format("{0}d {1}h {2}m", span.Days, span.Hours, span.Minutes);
            }
        }

        public string TimeColor
        {
            get
            {
                TimeSpan span = (DateTime.Now - CreatedDate);

                if(span.Days > 5)
                {
                    return "text-danger";
                }
                else if(span.Days > 2)
                {
                    return "text-warning";
                }
                else
                {
                    return "text-info";
                }
            }
        }

        public string AssignedTo { get; internal set; }

        public string AccountManager { get; internal set; }

        public IList<CommentsViewModel> CommentsList { get; internal set; }

        public IList<ActivityViewModel> ActivityList { get; internal set; }

        public OnboardingDetails Details { get; set; }

        public IList<ChecklistViewModel> EnrollmentCheckList { get; set; }

        public IList<ChecklistViewModel> VendorCheckList { get; set; }

        public IList<ChecklistViewModel> ClientCheckList { get; set; }
        public string EnrollChecklistString { get; internal set; }
        public int VendorId { get; internal set; }
        public string VendorChecklistString { get; internal set; }
        public int ClientId { get; internal set; }
        public string ClientChecklistString { get; internal set; }
    }
}
