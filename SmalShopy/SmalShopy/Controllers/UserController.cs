using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmalShopy.Models;
using SmalShopy.Services;


namespace SmalShopy.Controllers
{
    //api/Exercise/exercise1

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation(1, $"Getting user");
            User user = null;
            try
            {
                user = _userService.GetUserName();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
