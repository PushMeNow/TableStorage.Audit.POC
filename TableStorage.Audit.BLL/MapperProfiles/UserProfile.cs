using AutoMapper;
using TableStorage.Audit.BLL.Requests;
using TableStorage.Audit.BLL.Responses;
using TableStorage.Audit.DAL.Entities;

namespace TableStorage.Audit.BLL.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();

            CreateMap<AddressRequest, Address>();
            CreateMap<Address, AddressResponse>();
        }
    }
}