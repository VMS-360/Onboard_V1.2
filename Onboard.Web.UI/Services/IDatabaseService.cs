using System.Collections.Generic;
using Onboard.Web.UI.Models.DatabaseViewModels;
using Onboard.Web.UI.Models.HRViewModels;

namespace Onboard.Web.UI.Services
{
    public interface IDatabaseService
    {
        IList<ClientsViewModel> GetClients(int productOwnerId);
        ClientsViewModel GetClientDetails(int clientId);
        bool AddClient(int productOwnerId, ClientsViewModel client);
        IList<VendorsViewModel> GetVendors(int productOwnerId);
        VendorsViewModel GetVendorDetails(int vendorId);
        bool AddVendor(int productOwnerId, VendorsViewModel vendor);
        bool AddClientContact(ClientContactViewModel contact);
    }
}