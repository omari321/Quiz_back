using Infrastructure.Entities.User.Dtos;
using Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserManagment
{
    public interface IUserManagmentService
    {
        public Task<UserDto> CreateUser(CreateDto model);
        public Task<UserDto> UpdateUser(UpdateDto model);
        public Task<UserDto> LoginWithUser(string UserName);
        public Task<UserDto> DisableUser(int userId);
        public Task<bool> DeleteUser(string UserName);
        public Task<PageReturnDto<UserDto>> ListUsers(UserParameters model);
    }
}
