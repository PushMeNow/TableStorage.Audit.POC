using System;

namespace TableStorage.Audit.DAL.Entities
{
    public class User : BaseFields
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Address Address { get; set; }
    }
}