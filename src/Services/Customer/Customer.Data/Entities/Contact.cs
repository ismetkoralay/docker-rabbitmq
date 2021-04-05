
namespace Customer.Data.Entities
{
    public class Contact : BaseEntity
    {
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}