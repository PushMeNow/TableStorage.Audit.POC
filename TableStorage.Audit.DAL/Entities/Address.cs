using System;

namespace TableStorage.Audit.DAL.Entities
{
    public class Address : BaseFields
    {
        public Guid AddressId { get; set; }

        public Guid UserId { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public User User { get; set; }
    }
}