using Onboard.Web.UI.Models.HRViewModels;
using System;
using System.Collections.Generic;

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
        IList<CandidateViewModel> GetAllOnboardedCandidates(int productOwnerId, int enrollmentId);
        CandidateViewModel GetConsultantDetails(int enrollmentId);
        IList<DateTime?> GetOnboardDates(int productOwnerId);
        IList<CandidateViewModel> GetDeclinedCandidates(int productOwnerId);
    }
}