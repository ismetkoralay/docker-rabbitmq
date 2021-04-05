using System;

namespace Customer.Core
{
    public class CustomerContext
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}