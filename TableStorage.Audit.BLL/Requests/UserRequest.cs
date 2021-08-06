namespace TableStorage.Audit.BLL.Requests
{
    public class UserRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public AddressRequest Address { get; set; }
    }
}