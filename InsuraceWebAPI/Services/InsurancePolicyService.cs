using AutoMapper;
using InsuranceWebAPI.Interfaces;
using InsuranceWebAPI.Models.Dto;
using InsuranceWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceWebAPI.Services
{
    public class InsurancePolicyService : IInsurancePolicyService
    {
        private readonly IInsurancePolicyRepository _policyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public InsurancePolicyService(IInsurancePolicyRepository policyRepository, IUserRepository userRepository, IMapper mapper)
        {
            _policyRepository = policyRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<IEnumerable<InsurancePolicyDto>>> GetAllInsurancePolicies()
        {
            var policies = await _policyRepository.GetAllInsurancePolicies();
            var policyDtos = _mapper.Map<IEnumerable<InsurancePolicyDto>>(policies);
            return new ResponseDto<IEnumerable<InsurancePolicyDto>>
            {
                Data = policyDtos,
                IsSuccess = true,
                Message = "Policies retrieved successfully."
            };
        }

        public async Task<ResponseDto<InsurancePolicyDto>> GetInsurancePolicyById(int id)
        {
            var policy = await _policyRepository.GetInsurancePolicyById(id);
            if (policy == null)
            {
                return new ResponseDto<InsurancePolicyDto>
                {
                    IsSuccess = false,
                    Message = "Policy not found."
                };
            }

            var policyDto = _mapper.Map<InsurancePolicyDto>(policy);
            return new ResponseDto<InsurancePolicyDto>
            {
                Data = policyDto,
                IsSuccess = true,
                Message = "Policy retrieved successfully."
            };
        }

        public async Task<ResponseDto<InsurancePolicyDto>> AddInsurancePolicy(InsurancePolicyDto policyDto)
        {
            var response = new ResponseDto<InsurancePolicyDto>();

            try
            {
                if (ValidateInsurancePolicyFields(policyDto))
                {
                    var user = await _userRepository.GetUserById(policyDto.UserID);
                    if (user!=null)
                    {
                        var policy = _mapper.Map<InsurancePolicy>(policyDto);
                        await _policyRepository.AddInsurancePolicy(policy);

                        var addedPolicyDto = _mapper.Map<InsurancePolicyDto>(policy);
                        response.Data = addedPolicyDto;
                        response.IsSuccess = true;
                        response.Message = "Policy added successfully.";
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = $"Policy was not added - user not found";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Policy was not added - policy fields values incorrect";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Policy was not added - {ex.Message}";
            }

            return response;
        }

        public async Task<ResponseDto<InsurancePolicyDto>> UpdateInsurancePolicy(InsurancePolicyDto policyDto)
        {
            ResponseDto<InsurancePolicyDto> response = new ResponseDto<InsurancePolicyDto>();

            try
            {
                if (ValidateInsurancePolicyFields(policyDto))
                {
                    var existingPolicy = await _policyRepository.GetInsurancePolicyById(policyDto.ID);
                    if (existingPolicy != null)
                    {
                        if (existingPolicy.UserID == policyDto.UserID)
                        {
                            if (existingPolicy.PolicyNumber != policyDto.PolicyNumber)
                            {
                                response.IsSuccess = false;
                                response.Message = "Policy Number cannot be changed.";
                            }
                            else
                            {
                                var policy = _mapper.Map<InsurancePolicy>(policyDto);
                                await _policyRepository.UpdateInsurancePolicy(policy);
                                var updatedPolicyDto = _mapper.Map<InsurancePolicyDto>(policy);
                                response.Data = updatedPolicyDto;
                                response.IsSuccess = true;
                                response.Message = "Policy updated successfully.";
                            }
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "Policy was not updated - wrong UserID";
                        }
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Policy not found.";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Policy was not updated - policy fields values incorrect";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Policy was not updated - {ex.Message}";
            }

            return response;
        }

        public async Task<ResponseDto<bool>> DeleteInsurancePolicy(int id)
        {
            ResponseDto<bool> response;
            try
            {
                var existingPolicy = await _policyRepository.GetInsurancePolicyById(id);
                if (existingPolicy == null)
                {
                    response = new ResponseDto<bool>
                    {
                        IsSuccess = false,
                        Message = "Policy not found."
                    };
                }
                else
                {
                    await _policyRepository.DeleteInsurancePolicy(id);
                    response = new ResponseDto<bool>
                    {
                        Data = true,
                        IsSuccess = true,
                        Message = "Policy deleted successfully."
                    };
                }
            }
            catch (Exception ex)
            {
                response = new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = $"Policy was not deleted - {ex.Message}"
                };
            }

            return response;
        }

        public async Task<ResponseDto<IEnumerable<InsurancePolicyDto>>> GetInsurancePoliciesByUserID(int userID)
        {
            var policies = await _policyRepository.GetInsurancePoliciesByUserID(userID);
            var policyDtos = _mapper.Map<IEnumerable<InsurancePolicyDto>>(policies);
            return new ResponseDto<IEnumerable<InsurancePolicyDto>>
            {
                Data = policyDtos,
                IsSuccess = true,
                Message = policies.Any() ? "Policies retrieved successfully." : "No policies found for the specified user."
            };
        }

        private bool ValidateInsurancePolicyFields(InsurancePolicyDto policyDto)
        {
            return !(policyDto.InsuranceAmount < 0 || policyDto.StartDate > policyDto.EndDate || string.IsNullOrEmpty(policyDto.PolicyNumber));
        }
    }
}
