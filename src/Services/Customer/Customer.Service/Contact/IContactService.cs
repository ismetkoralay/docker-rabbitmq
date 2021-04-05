using System.Threading.Tasks;

namespace Customer.Service.Contact
{
    public interface IContactService
    {
        Task<BaseResponseModel> AddCustomerContact(int customerId, string address, string phone);
    }
}