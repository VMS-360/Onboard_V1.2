using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Onboard.Entities;
using Onboard.Web.UI.DataContexts;
using Onboard.Web.UI.Models;
using Onboard.Web.UI.Models.HRViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Onboard.Web.UI.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly OnboardDb _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public EnrollmentService(UserManager<ApplicationUser> userManager,
                                 RoleManager<ApplicationRole> roleManager,
                                 OnboardDb context)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;
        }

        public IList<CommentsViewModel> GetEnrollmentComments(int enrollmentId)
        {
            var comments = this._context
                                  .Enrollment_Comment
                                  .Where(r => r.EnrollmentId == enrollmentId)
                                  .OrderByDescending(r=>r.CreatedDate)
                                  .Select(r => new
                                  {
                                      Id = r.EnrollmentCommentId,
                                      Comment = r.Comment,
                                      CreatedDate = r.CreatedDate,
                                      CreatedBy = r.CreatedUser
                                  }).ToList();

            List<CommentsViewModel> returnList = new List<CommentsViewModel>();

            CommentsViewModel comment;
            foreach (var theComment in comments)
            {
                comment = new CommentsViewModel();

                comment.Id = theComment.Id;
                comment.Creator = "";
                comment.Role = "";
                if (!string.IsNullOrEmpty(theComment.CreatedBy))
                {
                    var user = this._userManager.Users.Where(r => r.UserName == theComment.CreatedBy).FirstOrDefault();
                    if (user != null)
                    {
                        comment.Creator = user.FirstName + " " + user.LastName;
                        var userRole = this._userManager
                                .Users
                                .Include(r => r.Roles)
                                .Where(r => r.UserName.ToUpper() == theComment.CreatedBy.ToUpper())
                                .Select(r => r.Roles.First()).ToList();
                        if (userRole != null && userRole.Count > 0)
                        {
                            comment.Role = this._roleManager.Roles.Where(r => r.Id == userRole.First().RoleId).First().Name;
                        }
                    }
                }

                comment.CreatedDate = theComment.CreatedDate == null ? new DateTime() : (DateTime)theComment.CreatedDate;
                comment.Comment = theComment.Comment;

                returnList.Add(comment);
            }

            return returnList;
        }

        public IList<ActivityViewModel> GetEnrollmentActivity(int enrollmentId)
        {
            var activity = this._context
                                 .Enrollment_Activity
                                 .Where(r => r.EnrollmentId == enrollmentId)
                                 .OrderByDescending(r => r.CreatedDate)
                                 .Select(r => new
                                 {
                                     Id = r.EnrollmentActivityId,
                                     Activity = r.Action,
                                     CreatedDate = r.CreatedDate
                                 }).ToList();

            return activity.Select(r => new ActivityViewModel
            {
                Id = r.Id,
                CreatedDate = r.CreatedDate == null ? new DateTime() : (DateTime)r.CreatedDate,
                Activity = r.Activity
            }).ToList();
        }

        public OnboardingDetails GetEditCandidateDetails(int enrollmentId)
        {
            var enrollment = this._context
                                  .Enrollment
                                  .Where(r => r.EnrollmentId == enrollmentId)
                                  .Select(r => new 
                                  {
                                      Email = r.Candidate.Email,
                                      AddressLine1 = r.Candidate.AddressLine1,
                                      AddressLine2 = r.Candidate.AddressLine2,
                                      City = r.Candidate.City,
                                      State = r.Candidate.State,
                                      Zip = r.Candidate.Zip,
                                      Phone = r.Candidate.Phone,
                                      Position = r.JobTitle,
                                      BillRate = r.BillRate == null ? "" : r.BillRate.ToString(),
                                      PayRate = r.PayRate == null ? "" : r.PayRate.ToString(),
                                      StartDate = r.StartDate == null ? "" : r.StartDate.ToString(),
                                      EndDate = r.EndDate == null ? "" : r.EndDate.ToString(),
                                      VendorId = r.VendorId,
                                      VendorContactId = r.VendorContactId,
                                      HrUserId = r.HRUserId,
                                      FirstName = r.Candidate.FirstName,
                                      LastName = r.Candidate.LastName,
                                  }).ToList();

            OnboardingDetails detail = new OnboardingDetails();
            foreach(var theDetail in enrollment)
            {
                detail.EnrollmentId = enrollmentId;
                detail.Email = theDetail.Email;
                detail.AddressLine1 = theDetail.AddressLine1;
                detail.AddressLine2 = theDetail.AddressLine2;
                detail.City = theDetail.City;
                detail.State = theDetail.State;
                detail.Zip = theDetail.Zip;
                detail.Phone = theDetail.Phone;
                detail.Position = theDetail.Position;
                detail.BillRate = theDetail.BillRate;
                detail.PayRate = theDetail.PayRate;
                detail.StartDate = theDetail.StartDate;
                detail.EndDate = theDetail.EndDate;
                detail.VendorId = theDetail.VendorId == null ? 0 : (int)theDetail.VendorId;
                detail.VendorContactId = theDetail.VendorContactId == null ? 0 : (int)theDetail.VendorContactId;
                detail.HrUserId = theDetail.HrUserId == null ? 0 : (int)theDetail.HrUserId;
                detail.CandidateFullName = theDetail.FirstName + " " + theDetail.LastName;
            }

            // Load Vendor details
            if(detail.VendorId != 0)
            {
                var vendor = this._context.Vendor.Where(r => r.VendorId == detail.VendorId).Select
                    (r => new
                    {
                        VendorName = r.CompanyName,
                        VendorAddressLine1 = r.AddressLine1,
                        VendorAddressLine2 = r.AddressLine2,
                        VendorCity = r.City,
                        VendorState = r.State,
                        VendorZip = r.Zip,
                        FederalId = r.TaxId,
                    }).ToList();

                foreach (var theVendor in vendor)
                {
                    detail.VendorName = theVendor.VendorName;
                    detail.VendorAddressLine1 = theVendor.VendorAddressLine1;
                    detail.VendorAddressLine2 = theVendor.VendorAddressLine2;
                    detail.VendorCity = theVendor.VendorCity;
                    detail.VendorState = theVendor.VendorState;
                    detail.VendorZip = theVendor.VendorZip;
                    detail.FederalId = theVendor.FederalId;
                }
            }

            // Load Vendor details
            if (detail.VendorContactId != 0)
            {
                var contact = this._context.Vendor_Contact.Where(r => r.VendorContactId == detail.VendorContactId).Select
                    (r => new
                    {
                        ContactFirstName = r.Name,
                        //ContactLastName = r.ContactLastName,
                        ContactEmail = r.Email,
                    }).ToList();

                foreach (var theContact in contact)
                {
                    detail.ContactFirstName = theContact.ContactFirstName;
                    detail.ContactEmail = theContact.ContactEmail;
                }
            }

            return detail;
        }

        public void UpdateCandidateDetails(OnboardingDetails detail)
        {
            var enrollment = this._context
                                  .Enrollment.Include(r=>r.Candidate).Include(r=>r.Vendor).Include(r => r.VendorContact)
                                  .Where(r => r.EnrollmentId == detail.EnrollmentId).First();
            enrollment.Candidate.Email = detail.Email;
            enrollment.Candidate.AddressLine1 = detail.AddressLine1;
            enrollment.Candidate.AddressLine2 = detail.AddressLine2;
            enrollment.Candidate.City = detail.City;
            enrollment.Candidate.State = detail.State;
            enrollment.Candidate.Zip = detail.Zip;
            enrollment.Candidate.Phone = detail.Phone;
            enrollment.JobTitle = detail.Position;
            enrollment.BillRate = Convert.ToDecimal(detail.BillRate);
            enrollment.PayRate = Convert.ToDecimal(detail.PayRate);
            enrollment.StartDate = Convert.ToDateTime(detail.StartDate);
            enrollment.EndDate = Convert.ToDateTime(detail.EndDate);
            enrollment.CurrentUser = detail.CurrentUser;

            if(enrollment.Vendor != null)
            {
                enrollment.Vendor.CompanyName = detail.VendorName;
                enrollment.Vendor.AddressLine1 = detail.VendorAddressLine1;
                enrollment.Vendor.AddressLine2 = detail.VendorAddressLine2;
                enrollment.Vendor.City = detail.VendorCity;
                enrollment.Vendor.State = detail.VendorState;
                enrollment.Vendor.Zip = detail.VendorZip;
                enrollment.Vendor.TaxId = detail.FederalId;
            }

            if (enrollment.VendorContact != null)
            {
                enrollment.VendorContact.Name = detail.ContactFirstName;
                enrollment.VendorContact.Email = detail.ContactEmail;
            }
            else if(!string.IsNullOrEmpty(detail.ContactFirstName) && enrollment.Vendor!=null)
            {
                VendorContact theContact = new VendorContact
                {
                    Name = detail.ContactFirstName,
                    Email = detail.ContactEmail,
                    VendorId = enrollment.Vendor.VendorId
                };

                this._context.Vendor_Contact.Add(theContact);
                enrollment.VendorContact = theContact;
            }

            this._context.Attach(enrollment);

            EnrollmentActivity activity = new EnrollmentActivity()
            {
                Action = "Candidate information altered",
                EnrollmentId = enrollment.EnrollmentId,
                Enrollment = enrollment
            };

            this._context.Enrollment_Activity.Add(activity);

            this._context.SaveChanges();
        }

        public void AddComment(CommentViewModel comment)
        {
            //var comments = this._context
            //                       .Enrollment_Comment
            //                       .Where(r => r.EnrollmentId == comment.EnrollmentId);

            EnrollmentComment theComment = new EnrollmentComment();
            theComment.EnrollmentId = comment.CommentEnrollmentId;
            theComment.Comment = comment.Comment;
            theComment.CurrentUser = comment.CurrentUser;

            this._context.Enrollment_Comment.Add(theComment);

            EnrollmentActivity activity = new EnrollmentActivity()
            {
                Action = "Comment made on candidate",
                EnrollmentId = comment.CommentEnrollmentId
            };

            this._context.Enrollment_Activity.Add(activity);
            this._context.SaveChanges();
        }

        public void AddEnrollmentActivity(int enrollmentId, string activityText, int hrUserId, string currentUser)
        {
            EnrollmentActivity activity = new EnrollmentActivity()
            {
                Action = activityText,
                EnrollmentId = enrollmentId,
                CurrentUser = currentUser
            };

            this._context.Enrollment_Activity.Add(activity);
            this._context.SaveChanges();
        }
    }
}