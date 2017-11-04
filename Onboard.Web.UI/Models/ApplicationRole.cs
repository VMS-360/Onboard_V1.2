using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Onboard.Web.UI.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole() { }

        public ApplicationRole(string name)
         : this()
        {
            this.Name = name;
        }

        public string IsActive { get; set; }
    }
}
