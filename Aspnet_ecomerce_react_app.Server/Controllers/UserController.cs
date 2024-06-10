using Aspnet_ecomerce_react_app.Server.Models;
using Aspnet_ecomerce_react_app.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aspnet_ecomerce_react_app.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.GetAllUsersAsync());
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            var addedUser = await _userService.AddUserAsync(user);
            if (addedUser != null)
            {
                return Ok(addedUser);
            }
            else
            {
                return BadRequest("Failed to add user.");
            }
        }

    }
}
