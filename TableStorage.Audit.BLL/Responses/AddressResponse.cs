using System;

namespace TableStorage.Audit.BLL.Responses
{
    public class AddressResponse : BaseFieldsResponse
    {
        public Guid AddressId { get; set; }

        public Guid UserId { get; set; }

        public string Country { get; set; }

        public string City { get; set; }
    }
}