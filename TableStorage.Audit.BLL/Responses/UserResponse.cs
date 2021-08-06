using System;

namespace TableStorage.Audit.BLL.Responses
{
    public class UserResponse : BaseFieldsResponse
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public AddressResponse Address { get; set; }
    }
}