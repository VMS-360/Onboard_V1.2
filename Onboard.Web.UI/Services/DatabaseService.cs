using Onboard.Entities;
using Onboard.Web.UI.DataContexts;
using Onboard.Web.UI.Models.DatabaseViewModels;
using Onboard.Web.UI.Models.HRViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Onboard.Web.UI.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly OnboardDb _context;

        public DatabaseService(OnboardDb context)
        {
            this._context = context;
        }

        public IList<ClientsViewModel> GetClients(int productOwnerId)
        {
            IList<ClientsViewModel> clients = this._context.Client.OrderBy(r => r.CompanyName).Select(r => new ClientsViewModel
            {
                ClientId = r.ClientId,
                CompanyName = r.CompanyName,
                Status = "A"
            }).ToList();

            return clients;
        }

        public ClientsViewModel GetClientDetails(int clientId)
        {
            ClientsViewModel client = this._context
                                          .Client
                                          .Where(r => r.ClientId == clientId)
                                          .Select(r => new ClientsViewModel
                                          {
                                              ClientId = r.ClientId,
                                              CompanyName = r.CompanyName,
                                              AddressLine1 = r.AddressLine1,
                                              AddressLine2 = r.AddressLine2,
                                              City = r.City,
                                              State = r.State,
                                              Zip = r.Zip,
                                              TaxId = r.TaxId
                                          }).FirstOrDefault();

            client.ConsultantsCount = this._context.Client_Contact.Where(r => r.ClientId == clientId).Count();
            client.ConsultantsCount = this._context.Enrollment.Where(r => r.ClientId == clientId).Select(r => r.CandidateId).Distinct().Count();
            client.EndClientsCount = this._context.Enrollment.Where(r => r.ClientId == clientId).Select(r => r.EndClientId).Distinct().Count();

            return client;
        }

        public bool AddClient(int productOwnerId, ClientsViewModel client)
        {
            bool success = false;

            int count = this._context
                            .Client
                            .Where(r => r.ProductOwnerId == productOwnerId && r.CompanyName.ToLower() == client.CompanyName.ToLower())
                            .Count();

            if (count == 0)
            {
                success = true;

                Client theClient = new Client
                {
                    CompanyName = client.CompanyName,
                    TaxId = client.TaxId,
                    AddressLine1 = client.AddressLine1,
                    City = client.City,
                    State = client.State,
                    Zip = client.Zip,
                    ProductOwnerId = productOwnerId,
                    CurrentUser = client.CurrentUser
                };

                this._context.Client.Add(theClient);
                this._context.SaveChanges();
            }

            return success;
        }

        public IList<VendorsViewModel> GetVendors(int productOwnerId)
        {
            IList<VendorsViewModel> vendors = this._context.Vendor.OrderBy(r => r.CompanyName).Select(r => new VendorsViewModel
            {
                VendorId = r.VendorId,
                CompanyName = r.CompanyName,
                Status = "A"
            }).ToList();

            return vendors;
        }

        public VendorsViewModel GetVendorDetails(int vendorId)
        {
            VendorsViewModel vendor = this._context
                                          .Vendor
                                          .Where(r => r.VendorId == vendorId)
                                          .Select(r => new VendorsViewModel
                                          {
                                              VendorId = r.VendorId,
                                              CompanyName = r.CompanyName,
                                              AddressLine1 = r.AddressLine1,
                                              AddressLine2 = r.AddressLine2,
                                              City = r.City,
                                              State = r.State,
                                              Zip = r.Zip,
                                              TaxId = r.TaxId
                                          }).FirstOrDefault();

            vendor.ConsultantsCount = this._context.Enrollment.Where(r => r.VendorId == vendorId).Select(r => r.CandidateId).Distinct().Count();

            //Vendor Contatct
            if (vendorId != 0)
            {
                var contact = this._context.Vendor_Contact.Where(r => r.VendorId == vendorId && r.IsSignatory != "Y").Select
                    (r => new
                    {
                        Id = r.VendorContactId,
                        ContactFirstName = r.Name,
                        JobTitle = r.JobTitle,
                        ContactEmail = r.Email,
                        Phone = r.Phone
                    }).ToList();
                if (contact == null)
                {
                    contact = this._context.Vendor_Contact.Where(r => r.VendorId == vendorId && r.IsSignatory == "Y").Select
                    (r => new
                    {
                        Id = r.VendorContactId,
                        ContactFirstName = r.Name,
                        JobTitle = r.JobTitle,
                        ContactEmail = r.Email,
                        Phone = r.Phone
                    }).ToList();
                }

                foreach (var theContact in contact)
                {
                    vendor.ContactId = theContact.Id;
                    vendor.ContactName = theContact.ContactFirstName;
                    vendor.ContactTitle = theContact.JobTitle;
                    vendor.ContactEmail = theContact.ContactEmail;
                    vendor.ContactPhone = theContact.Phone;
                }

                var signatory = this._context.Vendor_Contact.Where(r => r.VendorId == vendorId && r.IsSignatory == "Y").Select
                    (r => new
                    {
                        Id = r.VendorContactId,
                        ContactFirstName = r.Name,
                        JobTitle = r.JobTitle,
                        ContactEmail = r.Email,
                        Phone = r.Phone
                    }).ToList();

                foreach (var theContact in signatory)
                {
                    vendor.SignatoryId = theContact.Id;
                    vendor.SignatoryName = theContact.ContactFirstName;
                    vendor.SignatoryTitle = theContact.JobTitle;
                    vendor.SignatoryEmail = theContact.ContactEmail;
                    vendor.SignatoryPhone = theContact.Phone;
                }
            }

            return vendor;
        }

        public bool AddVendor(int productOwnerId, VendorsViewModel vendor)
        {
            bool success = false;

            int count = this._context
                            .Vendor
                            .Where(r => r.ProductOwnerId == productOwnerId && r.CompanyName.ToLower() == vendor.CompanyName.ToLower())
                            .Count();

            if (count == 0)
            {
                success = true;

                Vendor theVendor = new Vendor
                {
                    CompanyName = vendor.CompanyName,
                    TaxId = vendor.TaxId,
                    AddressLine1 = vendor.AddressLine1,
                    City = vendor.City,
                    State = vendor.State,
                    Zip = vendor.Zip,
                    ProductOwnerId = productOwnerId,
                    CurrentUser = vendor.CurrentUser
                };

                this._context.Vendor.Add(theVendor);
                this._context.SaveChanges();
            }

            return success;
        }

        public bool AddClientContact(ClientContactViewModel contact)
        {
            bool success = false;

            int count = this._context
                            .Client_Contact
                            .Where(r => r.ClientId == contact.ClientId && r.Name.ToLower() == contact.Name.ToLower())
                            .Count();

            if (count == 0)
            {
                success = true;
                string phone = contact.CandiatePhone;
                if (!string.IsNullOrEmpty(phone))
                {
                    phone = phone.Replace("(", "");
                    phone = phone.Replace(")", "");
                    phone = phone.Replace("-", "");
                }

                ClientContact theContact = new ClientContact
                {
                    Name = contact.Name,
                    Email = contact.Email,
                    Phone = phone,
                    ClientId = contact.ClientId,
                    CurrentUser = contact.CurrentUser
                };

                this._context.Client_Contact.Add(theContact);
                this._context.SaveChanges();
            }

            return success;
        }

        //End Clients
        public IList<EndClientsViewModel> GetEndClients(int productOwnerId)
        {
            IList<EndClientsViewModel> End_clients = this._context.End_Client.OrderBy(r => r.CompanyName).Select(r => new EndClientsViewModel
            {
                EndClientId = r.EndClientId,
                CompanyName = r.CompanyName,
                Status = "A"
            }).ToList();

            return End_clients;
        }

        public EndClientsViewModel GetEndClientDetails(int EndclientId)
        {
            EndClientsViewModel End_client = this._context
                                          .End_Client
                                          .Where(r => r.EndClientId == EndclientId)
                                          .Select(r => new EndClientsViewModel
                                          {
                                              EndClientId = r.EndClientId,
                                              CompanyName = r.CompanyName,
                                              AddressLine1 = r.AddressLine1,
                                              AddressLine2 = r.AddressLine2,
                                              City = r.City,
                                              State = r.State,
                                              Zip = r.Zip,
                                              TaxId = r.TaxId
                                          }).FirstOrDefault();

            //End_client.ConsultantsCount = this._context.End_Client_Contact.Where(r => r.EndClientId == EndclientId).Count();
            End_client.ConsultantsCount = this._context.Enrollment.Where(r => r.EndClientId == EndclientId).Select(r => r.CandidateId).Distinct().Count();
            End_client.EndClientsCount = this._context.Enrollment.Where(r => r.EndClientId == EndclientId).Select(r => r.EndClientId).Distinct().Count();

            return End_client;
        }

        public bool AddEndClient(int productOwnerId, EndClientsViewModel client)
        {
            bool success = false;

            int count = this._context
                            .End_Client
                            .Where(r => r.ProductOwnerId == productOwnerId && r.CompanyName.ToLower() == client.CompanyName.ToLower())
                            .Count();

            if (count == 0)
            {
                success = true;

                EndClient theClient = new EndClient
                {
                    CompanyName = client.CompanyName,
                    TaxId = client.TaxId,
                    AddressLine1 = client.AddressLine1,
                    City = client.City,
                    State = client.State,
                    Zip = client.Zip,
                    ProductOwnerId = productOwnerId,
                    CurrentUser = client.CurrentUser
                };

                this._context.End_Client.Add(theClient);
                this._context.SaveChanges();
            }

            return success;
        }

        public void UpdateVendorDetails(VendorsViewModel model)
        {
            Vendor vendor = this._context
                                .Vendor                                
                                .Where(r => r.VendorId == model.VendorId)
                                .FirstOrDefault();

            if (vendor != null)
            {
                vendor.CompanyName = model.CompanyName;
                vendor.AddressLine1 = model.AddressLine1;
                vendor.City = model.City;
                vendor.State = model.State;
                vendor.Zip = model.Zip;
            }

            VendorContact contact = this._context
                                .Vendor_Contact
                                .Where(r => r.VendorContactId == model.ContactId)
                                .FirstOrDefault();

            if (contact != null)
            {
                contact.Name = model.ContactName;
                contact.JobTitle = model.ContactTitle;
                contact.Email = model.ContactEmail;
            }

            VendorContact signatory = this._context
                                .Vendor_Contact
                                .Where(r => r.VendorContactId == model.SignatoryId)
                                .FirstOrDefault();

            if (contact != null)
            {
                signatory.Name = model.SignatoryName;
                signatory.JobTitle = model.SignatoryTitle;
                signatory.Email = model.SignatoryEmail;
            }

            this._context.SaveChanges();
        }

        public List<ChecklistViewModel> GetVendorChecklist(int vendorId)
        {
            var checkList = this._context
                                          .Vendor_Checklist
                                          .Where(r => r.VendorId == vendorId)
                                          .Select(r => new ChecklistViewModel
                                          {
                                              Id = r.VendorChecklistId,
                                              Text = r.Text,
                                              CommentType = r.CommentType,
                                              IsActive = r.IsActive
                                          }).ToList();

            return checkList;
        }
    }
}
