using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Customer.Core;
using Customer.Data;
using Customer.Service.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Customer.Service.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Data.Entities.Customer> _customerRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerService(IRepository<Data.Entities.Customer> customerRepository, 
            IHttpClientFactory httpClientFactory)
        {
            _customerRepository = customerRepository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CustomerContext> AddNewCustomer(string email, string password)
        {
            var customer = new Data.Entities.Customer
            {
                Email = email,
                Password = password,
                CreatedAt = DateTime.Now,
                CreatedBy = 1,
                IsActive = true
            };

            await _customerRepository.InsertAsync(customer);
            await _customerRepository.SaveAllAsync();

            await SendWelcomingMail(email);
            return await GetCustomerById(customer.Id);
        }

        public async Task<CustomerContext> GetCustomerById(int customerId)
        {
            var customer = await _customerRepository.Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == customerId);
            if (customer == null)
                return null;

            return new CustomerContext
            {
                CustomerId = customer.Id,
                Email = customer.Email,
                CreatedAt = customer.CreatedAt
            };
        }

        private async Task SendWelcomingMail(string email)
        {
            var mail = new EmailModel
            {
                Email = email,
                Subject = "Welcome!!",
                Text = "Welcome to the club!!!"
            };
            var message = mail.Serialize();

            var request = new QueueMessageModel
            {
                Message = message,
                QueueName = "welcoming-mail"
            };
            var body = request.Serialize();

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            StringContent str = new StringContent(body, Encoding.UTF8, "application/json");
            await client.PostAsync("http://host.docker.internal:5001/api/v1/Queue", str);
        }
    }
}