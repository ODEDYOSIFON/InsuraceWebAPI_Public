using InsuranceWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceWebAPI.Interfaces
{
    public interface IInsurancePolicyRepository
    {
        Task<IEnumerable<InsurancePolicy>> GetAllInsurancePolicies();
        Task<InsurancePolicy> GetInsurancePolicyById(int id);
        Task AddInsurancePolicy(InsurancePolicy policy);
        Task UpdateInsurancePolicy(InsurancePolicy policy);
        Task DeleteInsurancePolicy(int id);
        Task<IEnumerable<InsurancePolicy>> GetInsurancePoliciesByUserID(int userID);
        Task<IEnumerable<InsurancePolicy>> GetInsurancePoliciesByName(string name);
      
    }
}
