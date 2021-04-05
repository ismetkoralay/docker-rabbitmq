using System;

namespace Customer.Data.Entities
{
    public class BaseEntity
    {
        public virtual int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}