using InsuranceWebAPI.Models;
using InsuranceWebAPI.Models.Dto;

namespace InsuraceWebAPI.Models.Dto
{
    public class UserWithInsurancePoliciesDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<InsurancePolicyDto> InsurancePolicies { get; set; } = new List<InsurancePolicyDto>();
    }
}
