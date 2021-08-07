using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TableStorage.Audit.BLL.Interfaces;
using TableStorage.Audit.BLL.Requests;
using TableStorage.Audit.BLL.Responses;

namespace TableStorage.Audit.POC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public Task<UserResponse[]> GetAll()
        {
            return _userService.GetAll();
        }
        
        [HttpPost]
        public Task<UserResponse> Create([FromBody] UserRequest request)
        {
            return _userService.Create(request);
        }
        
        [HttpPut("{id:guid}")]
        public Task<UserResponse> Update([FromRoute] Guid id, [FromBody] UserRequest request)
        {
            return _userService.Update(id, request);
        }
        
        [HttpDelete("{id:guid}")]
        public  Task Delete([FromRoute] Guid id)
        {
            return _userService.Delete(id);
        }
    }
}