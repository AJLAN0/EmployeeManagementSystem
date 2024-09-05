using BusinessLayer.DTOs;
using DataAccessLayer.Models;

namespace BusinessLayer.Services.Abstraction
{
    public interface IUserService
    {
        Task<ResponseDto<string>> LoginAsync(UserLogin userLogin);
        Task<ResponseDto<Guid>> RegisterEmployeeAsync(UserRegister user);
        public Task<ResponseDto<UserInfoDTO>> GetUserInfoAsync(Guid userId);

    }

}
