using System;

namespace Onboard.Web.UI.Models.MaintenanceViewModels
{
    public class OnboardRoleViewModel : IComparable<OnboardRoleViewModel>
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public int CompareTo(OnboardRoleViewModel other)
        {
            return this.RoleName.CompareTo(other.RoleName);
        }
    }
}
