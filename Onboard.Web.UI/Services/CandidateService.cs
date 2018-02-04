using Onboard.Entities;
using Onboard.Web.UI.DataContexts;
using Onboard.Web.UI.Models.HRViewModels;
using Onboard.Web.UI.Models.DatabaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Onboard.Web.UI.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly OnboardDb _context;

        public CandidateService(OnboardDb context)
        {
            this._context = context;
        }

        public IList<CandidateViewModel> GetPendingCandidates(int productOwnerId)
        {
            var enrollments = this._context
                                  .Enrollment
                                  .Where(r => r.HRUserId == null &&
                                              r.Candidate.ProductOwnerId == productOwnerId &&
                                              (string.IsNullOrEmpty(r.OnboardedIndicator) || r.OnboardedIndicator == "N"))
                                  .Select(r => new
                                  {
                                      EnrollmentId = r.EnrollmentId,
                                      CandidateId = r.Candidate.CandidateId,
                                      FirstName = r.Candidate.FirstName,
                                      LastName = r.Candidate.LastName,
                                      ClientName = r.Client.CompanyName,
                                      BillingType = r.TaxStatusCode,
                                      CreatedDate = r.CreatedDate,
                                      ModifiedDate = r.ModifiedDate,
                                      ModifiedBy = r.ModifiedUser,
                                      PercentComplete = 83,
                                      VendorName = r.Vendor.CompanyName,
                                      AssignedTo = r.HRUserId,
                                      AccountManager = r.ProtfolioManagerId,
                                  }).ToList();

            return enrollments.Select(r => new CandidateViewModel
            {
                EnrollmentId = r.EnrollmentId,
                CandidateId = r.CandidateId,
                FirstName = r.FirstName,
                LastName = r.LastName,
                ClientName = r.ClientName == null ? "" : r.ClientName,
                BillingType = r.BillingType,
                CreatedDate = r.CreatedDate == null ? new DateTime() : (DateTime)r.CreatedDate,
                ModifiedDate = r.ModifiedDate == null ? new DateTime() : (DateTime)r.ModifiedDate,
                ModifiedBy = r.ModifiedBy,
                PercentComplete = 83,
                VendorName = r.VendorName == null ? "" : r.VendorName,
                AssignedTo = r.AssignedTo == null ? "" : r.AssignedTo.ToString(),
                AccountManager = r.AccountManager == null ? "" : r.AccountManager.ToString(),
            }).ToList();
        }

        public IList<CandidateViewModel> GetMyCandidates(int userId, int productOwnerId)
        {
            var enrollments = this._context
                                  .Enrollment
                                  .Where(r => r.HRUserId == userId &&
                                              r.Candidate.ProductOwnerId == productOwnerId &&
                                              (string.IsNullOrEmpty(r.OnboardedIndicator) || r.OnboardedIndicator == "N")
                                              && r.InactiveDate == null)
                                  .Select(r => new
                                  {
                                      EnrollmentId = r.EnrollmentId,
                                      CandidateId = r.Candidate.CandidateId,
                                      FirstName = r.Candidate.FirstName,
                                      LastName = r.Candidate.LastName,
                                      ClientName = r.Client.CompanyName,
                                      BillingType = r.TaxStatusCode,
                                      CreatedDate = r.CreatedDate,
                                      ModifiedDate = r.ModifiedDate,
                                      ModifiedBy = r.ModifiedUser,
                                      PercentComplete = 83,
                                      VendorName = r.Vendor.CompanyName,
                                      AssignedTo = r.HRUserId,
                                      AccountManager = r.ProtfolioManagerId,
                                      VendorId = r.VendorId,
                                      ClientId = r.ClientId
                                  }).ToList();

            List<CandidateViewModel> returnList = enrollments.Select(r => new CandidateViewModel
            {
                EnrollmentId = r.EnrollmentId,
                CandidateId = r.CandidateId,
                FirstName = r.FirstName,
                LastName = r.LastName,
                ClientName = r.ClientName == null ? "" : r.ClientName,
                BillingType = r.BillingType,
                CreatedDate = r.CreatedDate == null ? new DateTime() : (DateTime)r.CreatedDate,
                ModifiedDate = r.ModifiedDate == null ? new DateTime() : (DateTime)r.ModifiedDate,
                ModifiedBy = r.ModifiedBy,
                PercentComplete = 83,
                VendorName = r.VendorName == null ? "" : r.VendorName,
                AssignedTo = r.AssignedTo == null ? "" : r.AssignedTo.ToString(),
                AccountManager = r.AccountManager == null ? "" : r.AccountManager.ToString(),
                ClientId = r.ClientId == null ? 0 : (int)r.ClientId,
                VendorId = r.VendorId == null ? 0 : (int)r.VendorId
            }).ToList();

            foreach (CandidateViewModel theModel in returnList)
            {
                theModel.TotalTasks = this._context.Checklist.Where(r => r.EnrollmentId == theModel.EnrollmentId).Count();
                theModel.RemainingTasks = this._context.Checklist.Where(r => r.EnrollmentId == theModel.EnrollmentId && r.IndCompleted != "Y").Count();
                theModel.TotalTasks += this._context.Client_Checklist.Where(r => r.EnrollmentId == theModel.EnrollmentId).Count();
                theModel.RemainingTasks += this._context.Client_Checklist.Where(r => r.EnrollmentId == theModel.EnrollmentId && r.IndCompleted != "Y").Count();
                if (theModel.VendorId != 0)
                {
                    theModel.TotalTasks += this._context.Vendor_Checklist.Where(r => r.VendorId == theModel.VendorId).Count();
                    theModel.RemainingTasks += this._context.Vendor_Checklist.Where(r => r.VendorId == theModel.VendorId && r.IndCompleted != "Y").Count();
                }

                if (theModel.TotalTasks != 0)
                {
                    theModel.PercentComplete = Convert.ToInt32(Math.Round((double)((decimal)(theModel.TotalTasks - theModel.RemainingTasks) / theModel.TotalTasks) * 100, 0));
                }
                else
                {
                    theModel.PercentComplete = 0;
                }
            }

            return returnList;
        }

        public IList<CandidateViewModel> GetOnboardCandidates(int userId, int productOwnerId)
        {
            var enrollments = this._context
                                  .Enrollment
                                  .Where(r => r.HRUserId == userId &&
                                              r.Candidate.ProductOwnerId == productOwnerId &&
                                              !string.IsNullOrEmpty(r.OnboardedIndicator) &&
                                              r.OnboardedIndicator == "Y")
                                  .Select(r => new
                                  {
                                      EnrollmentId = r.EnrollmentId,
                                      CandidateId = r.Candidate.CandidateId,
                                      FirstName = r.Candidate.FirstName,
                                      LastName = r.Candidate.LastName,
                                      ClientName = r.Client.CompanyName,
                                      BillingType = r.TaxStatusCode,
                                      CreatedDate = r.CreatedDate,
                                      ModifiedDate = r.ModifiedDate,
                                      ModifiedBy = r.ModifiedUser,
                                      PercentComplete = 83,
                                      VendorName = r.Vendor.CompanyName,
                                      AssignedTo = r.HRUserId,
                                      AccountManager = r.ProtfolioManagerId,
                                      OnboardedDate = r.OnboardedDate
                                  }).ToList();

            return enrollments.Select(r => new CandidateViewModel
            {
                EnrollmentId = r.EnrollmentId,
                CandidateId = r.CandidateId,
                FirstName = r.FirstName,
                LastName = r.LastName,
                ClientName = r.ClientName == null ? "" : r.ClientName,
                BillingType = r.BillingType,
                CreatedDate = r.CreatedDate == null ? new DateTime() : (DateTime)r.CreatedDate,
                ModifiedDate = r.ModifiedDate == null ? new DateTime() : (DateTime)r.ModifiedDate,
                ModifiedBy = r.ModifiedBy,
                PercentComplete = 83,
                VendorName = r.VendorName == null ? "" : r.VendorName,
                AssignedTo = r.AssignedTo == null ? "" : r.AssignedTo.ToString(),
                AccountManager = r.AccountManager == null ? "" : r.AccountManager.ToString(),
                OnboardedDate = r.OnboardedDate == null ? new DateTime() : (DateTime)r.ModifiedDate,
            }).ToList();
        }

        public IList<CandidateViewModel> GetDeclinedCandidates(int productOwnerId)
        {
            var enrollments = this._context
                                  .Enrollment
                                  .Where(r => r.Candidate.ProductOwnerId == productOwnerId &&
                                              r.InactiveDate != null)
                                  .Select(r => new
                                  {
                                      EnrollmentId = r.EnrollmentId,
                                      CandidateId = r.Candidate.CandidateId,
                                      FirstName = r.Candidate.FirstName,
                                      LastName = r.Candidate.LastName,
                                      ClientName = r.Client.CompanyName,
                                      CreatedDate = r.CreatedDate,
                                      ModifiedDate = r.ModifiedDate,
                                      ModifiedBy = r.ModifiedUser,
                                      AccountManager = r.ProtfolioManagerId,
                                      InactiveDate = r.InactiveDate
                                  }).ToList();

            return enrollments.Select(r => new CandidateViewModel
            {
                EnrollmentId = r.EnrollmentId,
                CandidateId = r.CandidateId,
                FirstName = r.FirstName,
                LastName = r.LastName,
                CreatedDate = r.CreatedDate == null ? new DateTime() : (DateTime)r.CreatedDate,
                ModifiedDate = r.ModifiedDate == null ? new DateTime() : (DateTime)r.ModifiedDate,
                ModifiedBy = r.ModifiedBy,
                AccountManager = r.AccountManager == null ? "" : r.AccountManager.ToString(),
                InactiveDate = r.InactiveDate == null ? new DateTime() : (DateTime)r.InactiveDate
            }).ToList();
        }

        public CandidateDetailsViewModel GetCandateDetails(int enrollmentId)
        {
            var enrollment = this._context
                                  .Enrollment
                                  .Where(r => r.EnrollmentId == enrollmentId)
                                  .Select(r => new
                                  {
                                      FirstName = r.Candidate.FirstName,
                                      LastName = r.Candidate.LastName,
                                      ClientName = r.Client.CompanyName,
                                      EndClient = r.EndClient.CompanyName,
                                      BillingType = r.TaxStatusCode,
                                      CreatedDate = r.CreatedDate,
                                      VendorName = r.Vendor.CompanyName,
                                      AccountManager = r.ProtfolioManagerId,
                                      OnboardedIndicator = r.OnboardedIndicator,
                                      ClientContact = r.ClientContact.Name,
                                      InactiveDate = r.InactiveDate
                                  }).FirstOrDefault();

            return new CandidateDetailsViewModel
            {
                FirstName = enrollment.FirstName,
                LastName = enrollment.LastName,
                ClientName = enrollment.ClientName == null ? "" : enrollment.ClientName,
                BillingType = enrollment.BillingType,
                StatusIndicator = enrollment.OnboardedIndicator,
                AccountManager = enrollment.AccountManager == null ? "" : enrollment.AccountManager.ToString(),
                VendorName = enrollment.VendorName == null ? "" : enrollment.VendorName,
                EndClient = enrollment.EndClient == null ? "" : enrollment.EndClient,
                ClientContact = enrollment.ClientContact == null ? "" : enrollment.ClientContact,
                CreatedDate = enrollment.CreatedDate == null ? new DateTime() : (DateTime)enrollment.CreatedDate,
                Inactive = enrollment.InactiveDate != null ,
            };
        }

        public void AddCandidate(AddCandidateViewModel model, int productOwnerId)
        {
            var owner = this._context.Product_Owner.Where(r => r.ProductOwnerId == productOwnerId).First();
            //Add a Candidate
            Candidate candidate = new Candidate
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                //Gender = model.Gender.ToString(),
                Gender = "M",
                Email = model.Email,
                AddressLine1 = model.Address,
                City = model.City,
                State = model.State,
                Zip = model.Zip,
                ProductOwnerId = productOwnerId,
                ProductOwner = owner
            };

            if (!string.IsNullOrEmpty(model.SSN))
            {
                candidate.SSN = model.SSN.Replace("-", "");
            }

            if (!string.IsNullOrEmpty(model.Phone))
            {
                candidate.Phone = model.Phone.Replace("-", "").Replace("(", "").Replace(")", "");
            }

            this._context.Candidate.Add(candidate);

            int years = Convert.ToInt32(model.Duration.Split('-')[0]);
            int months = Convert.ToInt32(model.Duration.Split('-')[1]);

            Enrollment enrollment = new Enrollment()
            {
                Internal = model.Internal,
                JobTitle = model.JobTitle,
                DurationYears = years,
                DurationMonths = months,
                PayRate = Convert.ToDecimal(model.PayRate),
                //StartDate = model.StartDate,
                //EndDate = model.EndDate,
                OnboardedIndicator = "N",
                ActiveIndicator = "Y",
                EmploymentTypeCode = "1",
                TaxStatusCode = model.TaxStatus,
                ClientId = Convert.ToInt32(model.Client),
                ClientContactId = Convert.ToInt32(model.ClientContact),
                //VendorContactId = Convert.ToInt32(model.VendorContact)
            };

            if (!string.IsNullOrEmpty(model.BillRate) && model.Internal != "Internal")
            {
                enrollment.BillRate = Convert.ToDecimal(model.BillRate);
            }

            if (!string.IsNullOrEmpty(model.PortfolioManager) && model.Internal != "Internal")
            {
                enrollment.ProtfolioManagerId = Convert.ToInt32(model.PortfolioManager);
            }

            if (!string.IsNullOrEmpty(model.EndClient) && model.Internal != "Internal")
            {
                enrollment.EndClientId = Convert.ToInt32(model.EndClient);
            }

            if (!string.IsNullOrEmpty(model.Vendor) && model.Internal != "Internal")
            {
                if (model.TaxStatus != "W2" && model.TaxStatus != "1099")
                {
                    enrollment.VendorId = Convert.ToInt32(model.Vendor);
                }
            }

            enrollment.Candidate = candidate;
            //enrollment.EmploymentType = candidate;
            //enrollment.TaxStatus = candidate;
            //enrollment.Client = candidate;
            //enrollment.ClientContact = candidate;
            //enrollment.EndClient = candidate;
            //enrollment.Vendor = candidate;
            //enrollment.VendorContact = candidate;

            this._context.Enrollment.Add(enrollment);

            EnrollmentActivity activity = new EnrollmentActivity()
            {
                Action = "Candidate Created",
                Enrollment = enrollment
            };

            this._context.Enrollment_Activity.Add(activity);

            //Add Enrollment Checklist
            var enrollmentChecklist = this._context.Ref_Checklist.Where(r => r.EmploymentType == model.TaxStatus).Select(r => new { r.Text, r.CommentType, r.IsActive });
            foreach (var taskItem in enrollmentChecklist)
            {
                Checklist item = new Checklist
                {
                    Text = taskItem.Text,
                    IndCompleted = "N",
                    IsActive = taskItem.IsActive,
                    CommentType = taskItem.CommentType,
                    Enrollment = enrollment
                };

                this._context.Checklist.Add(item);
            }

            //Add Vendor Checklist
            if (enrollment.VendorId != 0)
            {
                var vendorChecklist = this._context.Ref_Checklist.Where(r => r.EmploymentType == "C2C").Select(r => new { r.Text, r.CommentType, r.IsActive });
                foreach (var taskItem in vendorChecklist)
                {
                    Checklist item = new Checklist
                    {
                        Text = taskItem.Text,
                        IndCompleted = "N",
                        IsActive = taskItem.IsActive,
                        CommentType = taskItem.CommentType,
                        Enrollment = enrollment
                    };

                    this._context.Checklist.Add(item);
                }

                int vendorCount = this._context.Vendor_Checklist.Where(r => r.VendorId == enrollment.VendorId).Count();

                if (vendorCount == 0)
                {
                    var vendorLookup = this._context.Ref_Checklist.Where(r => r.EmploymentType == "V").Select(r => new { r.Text, r.CommentType, r.IsActive });
                    foreach (var taskItem in vendorLookup)
                    {
                        VendorChecklist item = new VendorChecklist
                        {
                            Text = taskItem.Text,
                            IndCompleted = "N",
                            IsActive = taskItem.IsActive,
                            CommentType = taskItem.CommentType,
                            VendorId = Convert.ToInt32(model.Vendor)
                        };

                        this._context.Vendor_Checklist.Add(item);
                    }
                }
            }

            //Add Client Checklist
            if (enrollment.ClientId != 0)
            {
                var clientChecklist = this._context.Ref_Checklist.Where(r => r.ClientId == enrollment.ClientId).Select(r => new { r.Text, r.CommentType, r.IsActive });

                foreach (var taskItem in clientChecklist)
                {
                    ClientChecklist item = new ClientChecklist
                    {
                        Text = taskItem.Text,
                        IndCompleted = "N",
                        IsActive = taskItem.IsActive,
                        CommentType = taskItem.CommentType,
                        Enrollment = enrollment
                    };

                    this._context.Client_Checklist.Add(item);
                }
            }

            this._context.SaveChanges();
        }

        public void AssignEnrollment(int enrollmentId, int hrUserId, string currentUser)
        {
            var enrollment = this._context.Enrollment.Where(r => r.EnrollmentId == enrollmentId).FirstOrDefault();
            if (enrollment != null)
            {
                enrollment.HRUserId = hrUserId;
                enrollment.CurrentUser = currentUser;
            }

            this._context.SaveChanges();
        }

        public IList<CandidateViewModel> GetAllOnboardedCandidates(int productOwnerId, int enrollmentId)
        {
            var enrollments = this._context
                                  .Enrollment
                                  .Where(r => r.Candidate.ProductOwnerId == productOwnerId &&
                                  (enrollmentId == 0 || r.EnrollmentId == enrollmentId) &&
                                              r.OnboardedIndicator == "Y")
                                  .Select(r => new
                                  {
                                      EnrollmentId = r.EnrollmentId,
                                      CandidateId = r.Candidate.CandidateId,
                                      FirstName = r.Candidate.FirstName,
                                      LastName = r.Candidate.LastName,
                                      ClientName = r.Client.CompanyName,
                                      BillingType = r.TaxStatusCode,
                                      CreatedDate = r.CreatedDate,
                                      ModifiedDate = r.ModifiedDate,
                                      ModifiedBy = r.ModifiedUser,
                                      PercentComplete = 83,
                                      VendorName = r.Vendor.CompanyName,
                                      AssignedTo = r.HRUserId,
                                      AccountManager = r.ProtfolioManagerId,
                                      Email = r.Candidate.Email,
                                      StartDate = r.StartDate,
                                      EndDate = r.EndDate
                                      //TaxStatus = r.TaxStatus,
                                      //ClientContact = r.ClientContact
                                  }).ToList();

            List<CandidateViewModel> returnList = enrollments.Select(r => new CandidateViewModel
            {
                EnrollmentId = r.EnrollmentId,
                CandidateId = r.CandidateId,
                FirstName = r.FirstName,
                LastName = r.LastName,
                ClientName = r.ClientName == null ? "" : r.ClientName,
                BillingType = r.BillingType,
                CreatedDate = r.CreatedDate == null ? new DateTime() : (DateTime)r.CreatedDate,
                ModifiedDate = r.ModifiedDate == null ? new DateTime() : (DateTime)r.ModifiedDate,
                ModifiedBy = r.ModifiedBy,
                PercentComplete = 83,
                VendorName = r.VendorName == null ? "" : r.VendorName,
                AssignedTo = r.AssignedTo == null ? "" : r.AssignedTo.ToString(),
                AccountManager = r.AccountManager == null ? "" : r.AccountManager.ToString(),
                Email = r.Email,
                StartDate = r.StartDate == null ? new DateTime() : (DateTime)r.StartDate,
                EndDate = r.EndDate == null ? new DateTime() : (DateTime)r.EndDate
                //TaxStatus = r.TaxStatus == null ? "" : r.TaxStatus,
                //ClientContact = r.ClientContact == null ? "" : r.ClientContact

            }).ToList();

            foreach (CandidateViewModel theModel in returnList)
            {
                theModel.TotalTasks = this._context.Checklist.Where(r => r.EnrollmentId == theModel.EnrollmentId).Count();
                theModel.RemainingTasks = this._context.Checklist.Where(r => r.EnrollmentId == theModel.EnrollmentId && r.IndCompleted != "Y").Count();
                theModel.TotalTasks += this._context.Client_Checklist.Where(r => r.EnrollmentId == theModel.EnrollmentId).Count();
                theModel.RemainingTasks += this._context.Client_Checklist.Where(r => r.EnrollmentId == theModel.EnrollmentId && r.IndCompleted != "Y").Count();
                if (theModel.VendorId != 0)
                {
                    theModel.TotalTasks += this._context.Vendor_Checklist.Where(r => r.VendorId == theModel.VendorId).Count();
                    theModel.RemainingTasks += this._context.Vendor_Checklist.Where(r => r.VendorId == theModel.VendorId && r.IndCompleted != "Y").Count();
                }

                if (theModel.TotalTasks != 0)
                {
                    theModel.PercentComplete = Convert.ToInt32(Math.Round((double)((decimal)(theModel.TotalTasks - theModel.RemainingTasks) / theModel.TotalTasks) * 100, 0));
                }
                else
                {
                    theModel.PercentComplete = 0;
                }
            }

            return returnList;
        }

        public CandidateViewModel GetConsultantDetails(int enrollmentId)
        {
            var enrollments = this._context
                                  .Enrollment
                                  .Where(r => r.EnrollmentId == enrollmentId &&
                                              r.OnboardedIndicator == "Y")
                                  .Select(r => new
                                  {
                                      EnrollmentId = r.EnrollmentId,
                                      CandidateId = r.Candidate.CandidateId,
                                      FirstName = r.Candidate.FirstName,
                                      LastName = r.Candidate.LastName,
                                      ClientName = r.Client.CompanyName,
                                      BillingType = r.TaxStatusCode,
                                      CreatedDate = r.CreatedDate,
                                      ModifiedDate = r.ModifiedDate,
                                      ModifiedBy = r.ModifiedUser,
                                      PercentComplete = 83,
                                      VendorName = r.Vendor.CompanyName,
                                      AssignedTo = r.HRUserId,
                                      AccountManager = r.ProtfolioManagerId,
                                      Email = r.Candidate.Email,
                                      StartDate = r.StartDate,
                                      EndDate = r.EndDate
                                      //TaxStatus = r.TaxStatus,
                                      //ClientContact = r.ClientContact
                                  }).FirstOrDefault();

            return new CandidateViewModel
            {
                EnrollmentId = enrollments.EnrollmentId,
                CandidateId = enrollments.CandidateId,
                FirstName = enrollments.FirstName,
                LastName = enrollments.LastName,
                ClientName = enrollments.ClientName == null ? "" : enrollments.ClientName,
                BillingType = enrollments.BillingType,
                CreatedDate = enrollments.CreatedDate == null ? new DateTime() : (DateTime)enrollments.CreatedDate,
                ModifiedDate = enrollments.ModifiedDate == null ? new DateTime() : (DateTime)enrollments.ModifiedDate,
                ModifiedBy = enrollments.ModifiedBy,
                PercentComplete = 83,
                VendorName = enrollments.VendorName == null ? "" : enrollments.VendorName,
                AssignedTo = enrollments.AssignedTo == null ? "" : enrollments.AssignedTo.ToString(),
                AccountManager = enrollments.AccountManager == null ? "" : enrollments.AccountManager.ToString(),
                Email = enrollments.Email,
                StartDate = enrollments.StartDate == null ? new DateTime() : (DateTime)enrollments.StartDate,
                EndDate = enrollments.EndDate == null ? new DateTime() : (DateTime)enrollments.EndDate
                //TaxStatus = r.TaxStatus == null ? "" : r.TaxStatus,
                //ClientContact = r.ClientContact == null ? "" : r.ClientContact
            };
        }

        public void OnboardEnrollment(int enrollmentId, int hrUserId, string currentUser)
        {
            var enrollment = this._context.Enrollment.Where(r => r.EnrollmentId == enrollmentId).FirstOrDefault();
            if (enrollment != null)
            {
                enrollment.OnboardedDate = DateTime.Now;
                enrollment.OnboardedIndicator = "Y";
                enrollment.CurrentUser = currentUser;
            }

            this._context.SaveChanges();
        }

        public void AbortEnrollment(int enrollmentId, int hrUserId, string currentUser)
        {
            var enrollment = this._context.Enrollment.Where(r => r.EnrollmentId == enrollmentId).FirstOrDefault();
            if (enrollment != null)
            {
                enrollment.InactiveDate = DateTime.Now;
                enrollment.CurrentUser = currentUser;
            }

            this._context.SaveChanges();
        }

        public IList<DateTime?> GetOnboardDates(int productOwnerId)
        {
            return this._context
                        .Enrollment
                        .Where(r => r.Candidate.ProductOwnerId == productOwnerId &&
                                    !string.IsNullOrEmpty(r.OnboardedIndicator) &&
                                    r.OnboardedIndicator == "Y")
                        .Select(r => r.OnboardedDate).ToList();

        }

        public IList<int?> GetHRPeningEnrollmentCounts(int productOwnerId)
        {
            return this._context
                       .Enrollment
                       .Where(r => r.Candidate.ProductOwnerId == productOwnerId)
                       .Select(r => r.HRUserId)
                       .ToList();
        }

        public IList<int?> GetHROnboardedEnrollmentCounts(int productOwnerId)
        {
            return this._context
                       .Enrollment
                       .Where(r => r.Candidate.ProductOwnerId == productOwnerId &&
                                    !string.IsNullOrEmpty(r.OnboardedIndicator) &&
                                    r.OnboardedIndicator == "Y" &&
                                    r.OnboardedDate >= DateTime.Today.AddDays(-30))
                       .Select(r => r.HRUserId)
                       .ToList();
        }

        public string GetVendorName(int vendorId)
        {
            string vendorName = string.Empty;
            if (vendorId != 0)
            {
                var vendor = this._context.Vendor.Where(r => r.VendorId == vendorId).FirstOrDefault();
                if (vendor != null)
                {
                    vendorName = vendor.CompanyName;
                }
            }

            return vendorName;
        }

        public string GetClientName(int clientId)
        {
            string vendorName = string.Empty;
            if (clientId != 0)
            {
                var vendor = this._context.Client.Where(r => r.ClientId == clientId).FirstOrDefault();
                if (vendor != null)
                {
                    vendorName = vendor.CompanyName;
                }
            }

            return vendorName;
        }

        public IList<CandidateViewModel> GetAllPendingCandidates(int productOwnerId)
        {
            var enrollments = this._context
                                  .Enrollment
                                  .Where(r => r.Candidate.ProductOwnerId == productOwnerId &&
                                              (string.IsNullOrEmpty(r.OnboardedIndicator) || r.OnboardedIndicator == "N")
                                              && r.InactiveDate == null)
                                  .Select(r => new
                                  {
                                      HRUserId = r.HRUserId,
                                      EnrollmentId = r.EnrollmentId,
                                      CandidateId = r.Candidate.CandidateId,
                                      FirstName = r.Candidate.FirstName,
                                      LastName = r.Candidate.LastName,
                                      ClientName = r.Client.CompanyName,
                                      BillingType = r.TaxStatusCode,
                                      CreatedDate = r.CreatedDate,
                                      ModifiedDate = r.ModifiedDate,
                                      ModifiedBy = r.ModifiedUser,
                                      VendorName = r.Vendor.CompanyName,
                                      AssignedTo = r.HRUserId,
                                      AccountManager = r.ProtfolioManagerId,
                                      VendorId = r.VendorId,
                                      ClientId = r.ClientId,
                                      TaxStatus = r.TaxStatus.Description
                                  }).ToList();

            List<CandidateViewModel> returnList = enrollments.Select(r => new CandidateViewModel
            {
                HRUserId = r.HRUserId == null ? 0 : (int) r.HRUserId,
                EnrollmentId = r.EnrollmentId,
                CandidateId = r.CandidateId,
                FirstName = r.FirstName,
                LastName = r.LastName,
                ClientName = r.ClientName == null ? "" : r.ClientName,
                BillingType = r.BillingType,
                CreatedDate = r.CreatedDate == null ? new DateTime() : (DateTime)r.CreatedDate,
                ModifiedDate = r.ModifiedDate == null ? new DateTime() : (DateTime)r.ModifiedDate,
                ModifiedBy = r.ModifiedBy,
                VendorName = r.VendorName == null ? "" : r.VendorName,
                AssignedTo = r.AssignedTo == null ? "" : r.AssignedTo.ToString(),
                AccountManager = r.AccountManager == null ? "" : r.AccountManager.ToString(),
                ClientId = r.ClientId == null ? 0 : (int)r.ClientId,
                VendorId = r.VendorId == null ? 0 : (int)r.VendorId,
                TaxStatusString = r.TaxStatus
            }).ToList();

            return returnList;
        }

        public IList<CandidateViewModel> GetAllAssignedCandidates(int productOwnerId)
        {
            var enrollments = this._context
                                  .Enrollment
                                  .Where(r => r.Candidate.ProductOwnerId == productOwnerId &&
                                              (string.IsNullOrEmpty(r.OnboardedIndicator) || r.OnboardedIndicator == "N")
                                              && r.InactiveDate == null)
                                  .Select(r => new
                                  {
                                      AssignedTo = r.HRUserId,
                                      CreatedDate = r.CreatedDate,
                                  }).ToList();

            List<CandidateViewModel> returnList = enrollments.Select(r => new CandidateViewModel
            {
                CreatedDate = r.CreatedDate == null ? new DateTime() : (DateTime)r.CreatedDate,
                AssignedTo = r.AssignedTo == null ? "" : r.AssignedTo.ToString()
            }).ToList();

            return returnList;
        }
    }
}
