using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onboard.Web.UI.Models.HRViewModels
{
    public class ActivityViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string TimeString
        {
            get
            {
                return string.Format("{0}:{1}", this.CreatedDate.Hour, this.CreatedDate.Minute);
            }
        }

        public string DateString
        {
            get
            {
                return string.Format("{0}.{1}.{2} ", this.CreatedDate.Month, this.CreatedDate.Day, this.CreatedDate.Year.ToString());
            }
        }

        public string Activity { get; set; }
    }
}
