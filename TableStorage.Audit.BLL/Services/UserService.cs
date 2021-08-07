using AutoMapper;
using TableStorage.Audit.BLL.Interfaces;
using TableStorage.Audit.BLL.Requests;
using TableStorage.Audit.BLL.Responses;
using TableStorage.Audit.DAL;
using TableStorage.Audit.DAL.Entities;

namespace TableStorage.Audit.BLL.Services
{
    public class UserService : ServiceBase<User, UserRequest, UserResponse>, IUserService
    {
        public UserService(ProjectDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}