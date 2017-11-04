using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onboard.Web.UI.Models.HRViewModels
{
    public class CommentsViewModel
    {
        public int Id { get; set; }

        public string Creator { get; set; }

        public string Role { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ElapsedTime
        {
            get
            {
                if (this.CreatedDate == new DateTime())
                {
                    return string.Empty;
                }
                else
                {
                    TimeSpan span = (DateTime.Now - this.CreatedDate);

                    if (span.Hours > 0)
                    {
                        return string.Format("{0}h {1}m ago", span.Hours, span.Minutes);
                    }
                    else
                    {
                        return string.Format("{0}m ago", span.Minutes);
                    }
                }
            }
        }

        public string DateString
        {
            get
            {
                return string.Format("{0}:{1} - {2}.{3}.{4} ", this.CreatedDate.Hour, this.CreatedDate.Minute, this.CreatedDate.Month, this.CreatedDate.Day, this.CreatedDate.Year.ToString());
            }
        }

        public string Comment { get; set; }
    }
}
