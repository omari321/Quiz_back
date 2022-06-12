using Application.Services.UserManagment;
using Infrastructure.Entities.User.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {   
        private readonly IUserManagmentService _userManagmentService;

        public UsersController(IUserManagmentService userManagmentService)
        {
            _userManagmentService = userManagmentService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser(CreateDto model)
        {
            return Ok(await _userManagmentService.CreateUser(model));
        }
        [HttpPost("[action]/{UserName}")]
        public async Task<IActionResult> LoginWithUsername(string UserName)
        {
            return Ok(await _userManagmentService.LoginWithUser(UserName));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateUser(UpdateDto model)
        {
            return Ok(await _userManagmentService.UpdateUser(model));
        }
        [HttpPost("[action]/{userId}")]
        public async Task<IActionResult> DisableUser([FromRoute]int userId)
        {
            return Ok(await _userManagmentService.DisableUser(userId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteUser(string UserName)
        {
            return Ok(await _userManagmentService.DeleteUser(UserName));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ListUsers([FromQuery] UserParameters model)
        {
            return Ok(await _userManagmentService.ListUsers(model));
        }
    }
}
