using System.Threading.Tasks;
using Customer.Core;

namespace Customer.Service.Customer
{
    public interface ICustomerService
    {
        Task<CustomerContext> AddNewCustomer(string email, string password);
        Task<CustomerContext> GetCustomerById(int customerId);
    }
}