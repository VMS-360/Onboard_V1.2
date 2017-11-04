//using Onboard.Web.UI.Models.HRViewModels;
//using System;
//using System.Collections.Generic;

//namespace Onboard.Web.UI.Services
//{
//    public class CandidateServiceMockup : ICandidateService
//    {
//        public IList<CandidateViewModel> GetPendingCandidates(string userId)
//        {
//            IList<CandidateViewModel> returnList = new List<CandidateViewModel>();

//            returnList.Add(new CandidateViewModel
//            {
//                CandidateId = 1,
//                FirstName = "John",
//                LastName = "Stamos",
//                ClientName = "Tech Mahindra",
//                BillingType = "C2C",
//                CreatedDate = DateTime.Now.AddDays(-7),
//                VendorName = "ABC Staffing",
//                PercentComplete = 60,
//                ModifiedDate = DateTime.Now.AddHours(-7).AddMinutes(-33),
//                ModifiedBy = "Jeffrey Powell",
//                AssignedTo = "Jeffrey Powell",
//                AccountManager = "Varu",
//            });

//            returnList.Add(new CandidateViewModel
//            {
//                CandidateId = 2,
//                FirstName = "Mary",
//                LastName = "Claire",
//                ClientName = "Cognizant",
//                BillingType = "W2",
//                CreatedDate = DateTime.Now.AddDays(-4),
//                VendorName = "",
//                PercentComplete = 40,
//                ModifiedDate = DateTime.Now.AddHours(-7).AddMinutes(-33),
//                ModifiedBy = "Jeffrey Powell",
//                AssignedTo = "",
//                AccountManager = "Varu",
//            });

//            returnList.Add(new CandidateViewModel
//            {
//                CandidateId = 3,
//                FirstName = "Elon",
//                LastName = "Musk",
//                ClientName = "Wipro",
//                BillingType = "1099",
//                CreatedDate = DateTime.Now.AddMinutes(-30),
//                VendorName = "",
//                PercentComplete = 20,
//                ModifiedDate = DateTime.Now.AddHours(-7).AddMinutes(-33),
//                ModifiedBy = "Jeffrey Powell",
//                AssignedTo = "",
//                AccountManager = "Varu",
//            });

//            return returnList;
//        }

//        public IList<CandidateViewModel> GetMyCandidates(string userId)
//        {
//            IList<CandidateViewModel> returnList = new List<CandidateViewModel>();

//            returnList.Add(new CandidateViewModel
//            {
//                CandidateId = 1,
//                FirstName = "John",
//                LastName = "Lenon",
//                ClientName = "Tech Mahindra",
//                BillingType = "C2C",
//                CreatedDate = DateTime.Now.AddDays(-6).AddHours(-7).AddMinutes(-54),
//                ModifiedDate = DateTime.Now.AddHours(-7).AddMinutes(-33),
//                ModifiedBy = "Jeffrey Powell",
//                PercentComplete = 83,
//                VendorName = "Cyberdyne",
//                AssignedTo = "",
//                AccountManager = "Varu",
//            });

//            returnList.Add(new CandidateViewModel
//            {
//                CandidateId = 2,
//                FirstName = "Jerry",
//                LastName = "Lewls",
//                ClientName = "Tech Mahindra",
//                BillingType = "W2",
//                CreatedDate = DateTime.Now.AddDays(-4).AddHours(-6).AddMinutes(-53),
//                ModifiedDate = DateTime.Now.AddHours(-3).AddMinutes(-54),
//                ModifiedBy = "Jeffrey Powell",
//                PercentComplete = 74,
//                VendorName = "",
//                AssignedTo = "",
//                AccountManager = "Varu",
//            });

//            returnList.Add(new CandidateViewModel
//            {
//                CandidateId = 3,
//                FirstName = "Shawn",
//                LastName = "Grady",
//                ClientName = "Tech Mahindra",
//                BillingType = "C2C",
//                CreatedDate = DateTime.Now.AddDays(-3).AddHours(-1).AddMinutes(-37),
//                ModifiedDate = DateTime.Now.AddHours(-1).AddMinutes(-33),
//                ModifiedBy = "Jeffrey Powell",
//                PercentComplete = 61,
//                VendorName = "Glenair",
//                AssignedTo = "",
//                AccountManager = "Varu",
//            });

//            returnList.Add(new CandidateViewModel
//            {
//                CandidateId = 4,
//                FirstName = "Chelsey",
//                LastName = "Charbeneau",
//                ClientName = "Wipro",
//                BillingType = "C2C",
//                CreatedDate = DateTime.Now.AddDays(-0).AddHours(-6).AddMinutes(-14),
//                ModifiedDate = DateTime.Now.AddHours(-2).AddMinutes(-15),
//                ModifiedBy = "Jeffrey Powell",
//                PercentComplete = 10,
//                VendorName = "Enencorp",
//                AssignedTo = "",
//                AccountManager = "Varu",
//            });

//            return returnList;
//        }

//        public IList<CandidateViewModel> GetOnboardCandidates(string userId)
//        {
//            IList<CandidateViewModel> returnList = new List<CandidateViewModel>();

//            returnList.Add(new CandidateViewModel
//            {
//                CandidateId = 1,
//                FirstName = "Chet",
//                LastName = "Steadman",
//                ClientName = "Tech Mahindra",
//                BillingType = "C2C",
//                CreatedDate = new DateTime(2017, 4, 8),
//                VendorName = "Tesla"
//            });

//            returnList.Add(new CandidateViewModel
//            {
//                CandidateId = 2,
//                FirstName = "Jack",
//                LastName = "Johnson",
//                ClientName = "Cognizant",
//                BillingType = "W2",
//                CreatedDate = new DateTime(2017, 4, 8),
//                VendorName = ""
//            });

//            returnList.Add(new CandidateViewModel
//            {
//                CandidateId = 3,
//                FirstName = "Elon",
//                LastName = "Musk",
//                ClientName = "Wipro",
//                BillingType = "1099",
//                CreatedDate = new DateTime(2017, 4, 8),
//                VendorName = ""
//            });

//            return returnList;
//        }
//    }
//}
