using System.Collections.Generic;
using Onboard.Web.UI.Models.HRViewModels;

namespace Onboard.Web.UI.Services
{
    public interface IChecklistService
    {
        IList<ChecklistViewModel> GetClientCheklist(int clientId);
        IList<ChecklistViewModel> GetEnrollmentCheklist(int enrollmentId, string type);
        IList<ChecklistViewModel> GetVendorCheklist(int enrollmentId, int vendorId);
        void UpdateEnrollmentChecklistItem(int checklistId, bool isChecked, string type, string value, string enrollmentId, string currentUser, string userName, bool isVendor = false);
        void UpdateVendorChecklistItem(int checklistId, bool isChecked, string type, string value, string tagHelper, string enrollmentId, string currentUser, string userName);
        void UpdateClientChecklistItem(int checklistId, bool isChecked, string type, string value, string enrollmentId, string currentUser, string userName);
    }
}