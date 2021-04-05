using System;
using System.Threading.Tasks;
using Customer.Core;
using Customer.Data;
using Microsoft.EntityFrameworkCore;

namespace Customer.Service.Contact
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Data.Entities.Contact> _contactRepository;
        private readonly IRepository<Data.Entities.Customer> _customerRepository;

        public ContactService(IRepository<Data.Entities.Contact> contactRepository, 
            IRepository<Data.Entities.Customer> customerRepository)
        {
            _contactRepository = contactRepository;
            _customerRepository = customerRepository;
        }

        public async Task<BaseResponseModel> AddCustomerContact(int customerId, string address, string phone)
        {
            var customer = await _customerRepository.Table.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == customerId && x.IsActive);

            if (customer == null)
                return new BaseResponseModel(404, message: "Customer Is Not Found!!");

            var contact = new Data.Entities.Contact
            {
                CustomerId = customerId,
                Address = address,
                Phone = phone,
                CreatedAt = DateTime.Now,
                IsActive = true,
                CreatedBy = 1
            };

            await _contactRepository.InsertAsync(contact);
            await _contactRepository.SaveAllAsync();

            var context = new CustomerContext
            {
                CustomerId = customerId,
                Address = address,
                Phone = phone,
                CreatedAt = customer.CreatedAt,
                Email = customer.Email
            };
            return new BaseResponseModel(200, context, "Contact Added!!");
        }
    }
}