
namespace Customer.Data.Entities
{
    public class Customer : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}