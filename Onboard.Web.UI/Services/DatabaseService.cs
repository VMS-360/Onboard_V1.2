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

            if(count == 0)
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
                string phone = contact.Phone;
                if(!string.IsNullOrEmpty(phone))
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
    }
}
