using BusinessLayer.DTOs;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IUserService userService,
                                  ILogger<AccountController> logger)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("employee-register")]
        [Authorize(Roles = RolesConstent.Manager)]
        public async Task<IActionResult> RegisterSupportTeam([FromBody] UserRegister userRegister)
        {
            var result = await _userService.RegisterEmployeeAsync(userRegister);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize]
        [HttpGet("User-Info")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = GetUserIdFromToken();

            var userInfo = await _userService.GetUserInfoAsync(userId);

            if (userInfo == null)
            {
                return NotFound("User not found");
            }

            return Ok(userInfo);
        }
        private Guid GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return Guid.Parse(userIdClaim.Value);
        }
    }
}
