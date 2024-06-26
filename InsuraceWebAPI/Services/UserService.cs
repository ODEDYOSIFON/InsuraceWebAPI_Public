using AutoMapper;
using InsuranceWebAPI.Interfaces;
using InsuranceWebAPI.Models.Dto;
using InsuranceWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InsuraceWebAPI.Models.Dto;

namespace InsuranceWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IInsurancePolicyRepository _insurancePolicyRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IInsurancePolicyRepository insurancePolicyRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _insurancePolicyRepository = insurancePolicyRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<IEnumerable<User>>> GetAllUsers()
        {
            var response = new ResponseDto<IEnumerable<User>>();
            try
            {
                var users = await _userRepository.GetAllUsers();
                response.Data = users;
                response.IsSuccess = true;
                response.Message = "Users retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error retrieving users: {ex.Message}";
            }

            return response;
        }

        public async Task<ResponseDto<UserWithInsurancePoliciesDto>> GetUserById(int id)
        {
            var response = new ResponseDto<UserWithInsurancePoliciesDto>();
            try
            {
                var user = await _userRepository.GetUserById(id);

                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found.";
                }
                else
                {
                    var userWithInsurancePoliciesDto = _mapper.Map<UserWithInsurancePoliciesDto>(user);
                    var userPolicies = await _insurancePolicyRepository.GetInsurancePoliciesByUserID(userWithInsurancePoliciesDto.ID);

                    if (userPolicies.Any())
                    {
                        userWithInsurancePoliciesDto.InsurancePolicies = _mapper.Map<ICollection<InsurancePolicyDto>>(userPolicies);
                    }

                    response.Data = userWithInsurancePoliciesDto;
                    response.IsSuccess = true;
                    response.Message = "User retrieved successfully.";

                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error retrieving user: {ex.Message}";
            }

            return response;
        }

        public async Task<ResponseDto<User>> AddUser(User user)
        {
            var response = new ResponseDto<User>();
            try
            {
                if (!string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.Name))
                {
                    await _userRepository.AddUser(user);
                    response.Data = user;
                    response.IsSuccess = true;
                    response.Message = "User added successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = $"cannot add user, data is missing";
                }
               
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error adding user: {ex.Message}";
            }

            return response;
        }

        public async Task<ResponseDto<User>> UpdateUser(User user)
        {
            var response = new ResponseDto<User>();
            try
            {
                var existingUser = await _userRepository.GetUserById(user.ID);
                if (existingUser == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found.";
                }
                else
                {
                    await _userRepository.UpdateUser(user);
                    response.Data = user;
                    response.IsSuccess = true;
                    response.Message = "User updated successfully.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error updating user: {ex.Message}";
            }

            return response;
        }

        public async Task<ResponseDto<bool>> DeleteUser(int id)
        {
            var response = new ResponseDto<bool>();
            try
            {
                var existingUser = await _userRepository.GetUserById(id);
                if (existingUser == null)
                {
                    response.IsSuccess = false;
                    response.Message = "User not found.";
                }
                else
                {
                    await _userRepository.DeleteUser(id);
                    response.Data = true;
                    response.IsSuccess = true;
                    response.Message = "User deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error deleting user: {ex.Message}";
            }

            return response;
        }
    }
}
