using BusinessLayer.DTOs;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Configuration;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IConfiguration _configuration;
        private readonly IRoleRepository _roleRepository;

        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IJwtTokenService jwtTokenService,
                           IConfiguration configuration,
                           IRoleRepository roleRepository,
                           IUserRepository userRepository,
                           ILogger<UserService> logger,
                           IPasswordHasher<User> passwordHasher)
        {
            _logger = logger;
            _jwtTokenService = jwtTokenService;
            _configuration = configuration;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public async Task<ResponseDto<string>> LoginAsync(UserLogin userLogin)
        {
            var user = await _userRepository.GetUserByEmailAsync(userLogin.Email);
            if (user == null)
            {
                return new ResponseDto<string>
                {
                    IsSuccess = false,
                    ErrorMessage = "Invalid Email ",
                    StatusCode = 400
                };
            }

            if (!user.IsActive)
            {
                return new ResponseDto<string>
                {
                    IsSuccess = false,
                    ErrorMessage = "You can not login because the user does not activated you should contact Manager ",
                    StatusCode = 400
                };

            }

            var passwordVerify = _passwordHasher.VerifyHashedPassword(user, user.Password, userLogin.Password);
            if (passwordVerify == PasswordVerificationResult.Failed)
            {
                return new ResponseDto<string>
                {
                    IsSuccess = false,
                    ErrorMessage = "Invalid  Password.",
                    StatusCode = 400
                };
            }

            var token = _jwtTokenService.GenerateToken(user);
            return new ResponseDto<string>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = token
            };
        }

        public async Task<ResponseDto<Guid>> RegisterEmployeeAsync(UserRegister userDto)
        {
            var existUser = await _userRepository.GetUserByEmailAsync(userDto.Email);

            if (existUser != null)
            {
                return new ResponseDto<Guid>
                {
                    IsSuccess = false,
                    ErrorMessage = "Email already in use!",
                };
            }

            var role = await _roleRepository.GetRoleAsync(RolesConstent.Employee);

            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                IsActive = true,
                Email = userDto.Email,
                Username = userDto.Username,
                Password = userDto.Password,
                Role = role
            };

            user.Password = _passwordHasher.HashPassword(user, userDto.Password);

            await _userRepository.AddUserAsync(user);

            return new ResponseDto<Guid>
            {
                IsSuccess = true,
                StatusCode = 200,
                Result = userId
            };
        }

        public async Task<ResponseDto<UserInfoDTO>> GetUserInfoAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return new ResponseDto<UserInfoDTO>
                {
                    IsSuccess = false,
                    ErrorMessage = "The User is not found "
                };
            }
            
            var userInfoDto = new UserInfoDTO
            {
                Id = user.Id,
                Username = user.Username,
                IsActive = user.IsActive,
                Email = user.Email,
                Role = user.Role.RoleName,
            };

            return new ResponseDto<UserInfoDTO>
            {
                IsSuccess = true,
                Result = userInfoDto,
                StatusCode = 200
            };

        }

    }
}
