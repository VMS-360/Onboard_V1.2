using Onboard.Web.UI.Models.HRViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onboard.Web.UI.Services
{
    public interface IEnrollmentService
    {
        IList<CommentsViewModel> GetEnrollmentComments(int enrollmentId);

        IList<ActivityViewModel> GetEnrollmentActivity(int enrollmentId);
        OnboardingDetails GetEditCandidateDetails(int enrollmentId);
        void UpdateCandidateDetails(OnboardingDetails detail);
        void AddComment(CommentViewModel comment);
    }
}
