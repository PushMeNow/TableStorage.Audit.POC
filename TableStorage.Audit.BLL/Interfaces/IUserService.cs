using TableStorage.Audit.BLL.Requests;
using TableStorage.Audit.BLL.Responses;

namespace TableStorage.Audit.BLL.Interfaces
{
    public interface IUserService : IService<UserRequest, UserResponse>
    {
    }
}