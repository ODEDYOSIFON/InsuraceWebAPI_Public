using InsuranceWebAPI.Models.Dto;
using InsuranceWebAPI.Models;
using InsuraceWebAPI.Models.Dto;

namespace InsuranceWebAPI.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDto<IEnumerable<User>>> GetAllUsers();
        Task<ResponseDto<UserWithInsurancePoliciesDto>> GetUserById(int id);
        Task<ResponseDto<User>> AddUser(User user);
        Task<ResponseDto<User>> UpdateUser(User user);
        Task<ResponseDto<bool>> DeleteUser(int id);
    }
}
