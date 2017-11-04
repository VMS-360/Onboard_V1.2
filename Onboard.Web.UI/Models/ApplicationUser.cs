using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Onboard.Web.UI.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int ProductOwnerId { get; set; }

        public string IsInternal { get; set; }

        public string PrimaryColor { get; set; }

        public string WarningColor { get; set; }

        public string WarningColor2 { get; set; }

        public string NotificationColor { get; set; }
    }
}
