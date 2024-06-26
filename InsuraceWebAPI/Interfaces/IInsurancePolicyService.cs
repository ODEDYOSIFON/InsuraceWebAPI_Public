using InsuranceWebAPI.Models.Dto;
using InsuranceWebAPI.Models;

namespace InsuranceWebAPI.Interfaces
{
    public interface IInsurancePolicyService
    {
        Task<ResponseDto<IEnumerable<InsurancePolicyDto>>> GetAllInsurancePolicies();
        Task<ResponseDto<InsurancePolicyDto>> GetInsurancePolicyById(int id);
        Task<ResponseDto<InsurancePolicyDto>> AddInsurancePolicy(InsurancePolicyDto policy);
        Task<ResponseDto<InsurancePolicyDto>> UpdateInsurancePolicy(InsurancePolicyDto policy);
        Task<ResponseDto<bool>> DeleteInsurancePolicy(int id);
        Task<ResponseDto<IEnumerable<InsurancePolicyDto>>> GetInsurancePoliciesByUserID(int UserID);
    }
}
