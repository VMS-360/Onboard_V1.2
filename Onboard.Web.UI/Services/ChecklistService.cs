using Onboard.Entities;
using Onboard.Web.UI.DataContexts;
using Onboard.Web.UI.Models.HRViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Onboard.Web.UI.Services
{
    public class ChecklistService : IChecklistService
    {
        private readonly OnboardDb _context;

        public ChecklistService(OnboardDb context)
        {
            this._context = context;
        }

        public IList<ChecklistViewModel> GetEnrollmentCheklist(int enrollmentId, string type)
        {
            var checkList = this._context
                                  .Checklist
                                  .Where(r => r.EnrollmentId == enrollmentId && r.IndVendor != "Y" && r.IsActive == "Y")
                                  .Select(r => new
                                  {
                                      Id = r.ChecklistId,
                                      IsChecked = r.IndCompleted,
                                      Text = r.Text,
                                      CommentType = r.CommentType,
                                      CommentValue = r.CommentValue
                                  }).ToList();

            var returnList = checkList.Select(r => new ChecklistViewModel
            {
                Id = r.Id,
                IsChecked = r.IsChecked == "Y" ? true : false,
                Text = r.Text,
                CommentType = r.CommentType,
                CommentValue = r.CommentValue,
                Helper = "E"
            }).ToList();

            return returnList;
        }

        public IList<ChecklistViewModel> GetVendorCheklist(int enrollmentId, int vendorId)
        {
            var enrollementList = this._context
                                  .Checklist
                                  .Where(r => r.EnrollmentId == enrollmentId && r.IndVendor == "Y" && r.IsActive == "Y")
                                  .Select(r => new
                                  {
                                      Id = r.ChecklistId,
                                      IsChecked = r.IndCompleted,
                                      Text = r.Text,
                                      CommentType = r.CommentType,
                                      CommentValue = r.CommentValue
                                  }).ToList();

            var enList = enrollementList.Select(r => new ChecklistViewModel
            {
                Id = r.Id,
                IsChecked = r.IsChecked == "Y" ? true : false,
                Text = r.Text,
                CommentType = r.CommentType,
                CommentValue = r.CommentValue
            }).ToList();

            var checkList = this._context
                                  .Vendor_Checklist
                                  .Where(r => r.VendorId == vendorId && r.IsActive == "Y")
                                  .Select(r => new
                                  {
                                      Id = r.VendorChecklistId,
                                      IsChecked = r.IndCompleted,
                                      Text = r.Text,
                                      CommentType = r.CommentType,
                                      CommentValue = r.CommentValue
                                  }).ToList();

            var returnList = checkList.Select(r => new ChecklistViewModel
            {
                Id = r.Id,
                IsChecked = r.IsChecked == "Y" ? true : false,
                Text = r.Text,
                CommentType = r.CommentType,
                CommentValue = r.CommentValue,
                Helper = "V"
            }).ToList();

            foreach(var theItem in enList)
            {
                returnList.Add(new ChecklistViewModel
                {
                    Id = theItem.Id,
                    IsChecked = theItem.IsChecked,
                    Text = theItem.Text,
                    CommentType = theItem.CommentType,
                    CommentValue = theItem.CommentValue,
                    Helper = "E"
                });
            }

            return returnList;
        }

        public IList<ChecklistViewModel> GetClientCheklist(int enrollmentId)
        {
            var checkList = this._context
                                  .Client_Checklist
                                  .Where(r => r.EnrollmentId == enrollmentId && r.IsActive == "Y")
                                  .Select(r => new
                                  {
                                      Id = r.ClientChecklistId,
                                      IsChecked = r.IndCompleted,
                                      Text = r.Text,
                                      CommentType = r.CommentType,
                                      CommentValue = r.CommentValue
                                  }).ToList();

            var returnList = checkList.Select(r => new ChecklistViewModel
            {
                Id = r.Id,
                IsChecked = r.IsChecked == "Y" ? true : false,
                Text = r.Text,
                CommentType = r.CommentType,
                CommentValue = r.CommentValue,
                Helper = "C"
            }).ToList();

            return returnList;
        }

        public void UpdateEnrollmentChecklistItem(int checklistId, bool isChecked, string type, string value, string enrollmentId, string currentUser, string userName, bool isVendor)
        {
            string completed = isChecked ? "Y" : "N";

            Checklist item = this._context.Checklist.Where(r => r.ChecklistId == checklistId).First();
            if (item != null)
            {
                item.IndCompleted = completed;
                item.CurrentUser = currentUser;

                if (type == "Text" || type == "Date")
                {
                    item.CommentValue = value;
                }
                else
                {
                    item.CommentValue = null;
                }
            }

            this._context.Checklist.Attach(item);

            if(!string.IsNullOrEmpty(enrollmentId))
            {
                int eid = Convert.ToInt32(enrollmentId);
                string status = isChecked ? "checked" : "unchecked";
                string vendorText = isVendor ? "Vendor" : "Enrollment";
                string text = vendorText + " tasklist item " + item.Text + " is " + status + " by " + userName;
                EnrollmentActivity activity = new EnrollmentActivity()
                {
                    Action = text,
                    EnrollmentId = eid,
                    CurrentUser = currentUser
                };

                this._context.Enrollment_Activity.Add(activity);
            }

            this._context.SaveChanges();
        }

        public void UpdateVendorChecklistItem(int checklistId, bool isChecked, string type, string value, string tagHelper, string enrollmentId, string currentUser, string userName)
        {
            string completed = isChecked ? "Y" : "N";

            if (tagHelper == "V")
            {
                VendorChecklist item = this._context.Vendor_Checklist.Where(r => r.VendorChecklistId == checklistId).First();
                if (item != null)
                {
                    item.IndCompleted = completed;
                    item.CurrentUser = currentUser;

                    if (type == "Text" || type == "Date")
                    {
                        item.CommentValue = value;
                    }
                    else
                    {
                        item.CommentValue = null;
                    }
                }

                this._context.Vendor_Checklist.Attach(item);

                if (!string.IsNullOrEmpty(enrollmentId))
                {
                    int eid = Convert.ToInt32(enrollmentId);
                    string status = isChecked ? "checked" : "unchecked";
                    string text = "Vendor tasklist item " + item.Text + " is " + status + " by " + userName;
                    EnrollmentActivity activity = new EnrollmentActivity()
                    {
                        Action = text,
                        EnrollmentId = eid,
                        CurrentUser = currentUser
                    };

                    this._context.Enrollment_Activity.Add(activity);
                }
                this._context.SaveChanges();
            }
            else
            {
                this.UpdateEnrollmentChecklistItem(checklistId, isChecked, type, value, enrollmentId, currentUser, userName, true);
            }
        }

        public void UpdateClientChecklistItem(int checklistId, bool isChecked, string type, string value, string enrollmentId, string currentUser, string userName)
        {
            string completed = isChecked ? "Y" : "N";

            ClientChecklist item = this._context.Client_Checklist.Where(r => r.ClientChecklistId == checklistId).First();
            if (item != null)
            {
                item.IndCompleted = completed;
                item.CurrentUser = currentUser;

                if (type == "Text" || type == "Date")
                {
                    item.CommentValue = value;
                }
                else
                {
                    item.CommentValue = null;
                }
            }

            this._context.Client_Checklist.Attach(item);

            if (!string.IsNullOrEmpty(enrollmentId))
            {
                int eid = Convert.ToInt32(enrollmentId);
                string status = isChecked ? "checked" : "unchecked";
                string text = "Client tasklist item " + item.Text + " is " + status + " by " + userName;
                EnrollmentActivity activity = new EnrollmentActivity()
                {
                    Action = text,
                    EnrollmentId = eid,
                    CurrentUser = currentUser
                };

                this._context.Enrollment_Activity.Add(activity);
            }
            this._context.SaveChanges();
        }
    }
}
