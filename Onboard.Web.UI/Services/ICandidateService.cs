using System.Collections.Generic;
using Onboard.Web.UI.Models.HRViewModels;

namespace Onboard.Web.UI.Services
{
    public interface ICandidateService
    {
        IList<CandidateViewModel> GetMyCandidates(int userId, int productOwnerId);
        IList<CandidateViewModel> GetOnboardCandidates(int userId, int productOwnerId);
        IList<CandidateViewModel> GetPendingCandidates(int productOwnerId);
        CandidateDetailsViewModel GetCandateDetails(int enrollmentId);
        void AddCandidate(AddCandidateViewModel model, int productOwnerId);
        void AssignEnrollment(int enrollmentId, int hrUserId, string currentUser);
        void OnboardEnrollment(int enrollmentId, int hrUserId, string currentUser);
    }
}