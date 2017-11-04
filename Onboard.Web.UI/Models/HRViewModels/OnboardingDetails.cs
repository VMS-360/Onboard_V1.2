using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Onboard.Web.UI.Models.HRViewModels
{
    public class OnboardingDetails
    {
        public int EnrollmentId { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Address
        {
            get
            {
                return string.Format("{0} {1} {2}, {3} {4}", this.AddressLine1, this.AddressLine2, this.City, this.State, this.Zip);
            }
        }

        [Required(ErrorMessage = "Address is required")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }

        public string PhoneString
        {
            get
            {
                if (Phone != null && Phone.Length >= 10)
                {
                    return string.Format("{0}.{1}.{2}", this.Phone.Substring(0, 3), this.Phone.Substring(3, 3), this.Phone.Substring(6, 4));
                }
                else
                {
                    return this.Phone;
                }
            }
        }

        public string Position { get; set; }
        public string BillRate { get; set; }
        public string PayRate { get; set; }
        [DataType(DataType.Date)]
        public string StartDate { get; set; }
        [DataType(DataType.Date)]
        public string EndDate { get; set; }
        public string Duration { get; set; }
        public string VendorName { get; set; }
        public string VendorAddressLine1 { get; set; }
        public string VendorAddressLine2 { get; set; }
        public string VendorCity { get; set; }
        public string VendorState { get; set; }
        public string VendorZip { get; set; }
        public string VendorAddress
        {
            get
            {
                return string.Format("{0} {1} {2}, {3} {4}", this.VendorAddressLine1, this.VendorAddressLine2, this.VendorCity, this.VendorState, this.VendorZip);
            }
        }

        public string FederalId { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactName
        {
            get
            {
                return string.Format("{0} {1}", this.ContactFirstName, this.ContactLastName);
            }
        }

        public string ContactEmail { get; set; }
        public string SignatoryFirstName { get; set; }
        public string SignatoryLastName { get; set; }
        public string SignatoryName
        {
            get
            {
                return string.Format("{0} {1}", this.SignatoryFirstName, this.SignatoryLastName);
            }
        }

        public string SignatoryEmail { get; set; }
        public int VendorId { get; internal set; }
        public int VendorContactId { get; internal set; }
        public string CurrentUser { get; internal set; }
    }
}
