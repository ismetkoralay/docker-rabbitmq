using System.Threading.Tasks;
using Customer.Api.Models.Customer;
using Customer.Core;
using Customer.Service.Contact;
using Customer.Service.Customer;
using Microsoft.AspNetCore.Mvc;

namespace Customer.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IContactService _contactService;

        public CustomerController(ICustomerService customerService, 
            IContactService contactService)
        {
            _customerService = customerService;
            _contactService = contactService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerContext>> GetById([FromRoute] int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
                return NotFound("Customer not found");
            return new OkObjectResult(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerContext>> AddNewCustomer([FromBody] AddNewCustomerRequestModel request)
        {
            var result = await _customerService.AddNewCustomer(request.Email, request.Password);
            return new OkObjectResult("Customer added");
        }

        [HttpPost("{id:int}/contact")]
        public async Task<ActionResult<CustomerContext>> AddCustomerContact([FromRoute] int id, [FromBody] AddCustomerContactRequestModel request)
        {
            return await _contactService.AddCustomerContact(id, request.Address, request.Phone);
        }
    }
}