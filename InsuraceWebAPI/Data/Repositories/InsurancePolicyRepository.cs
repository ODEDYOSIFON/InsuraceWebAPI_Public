using InsuranceWebAPI.Interfaces;
using InsuranceWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace InsuranceWebAPI.Data.Repositories
{
    public class InsurancePolicyRepository : IInsurancePolicyRepository
    {
        private readonly AppDbContext _context;

        public InsurancePolicyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InsurancePolicy>> GetAllInsurancePolicies() => await _context.InsurancePolicies.ToListAsync();

        public async Task<InsurancePolicy> GetInsurancePolicyById(int id)
        {
            return await _context.InsurancePolicies.FindAsync(id);
        }

        public async Task AddInsurancePolicy(InsurancePolicy policy)
        {
            await _context.InsurancePolicies.AddAsync(policy);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInsurancePolicy(InsurancePolicy policy)
        {
            var existingInsurancePolicy = await _context.InsurancePolicies.FindAsync(policy.ID);
            if (existingInsurancePolicy != null)
            {
                _context.Entry(existingInsurancePolicy).CurrentValues.SetValues(policy);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteInsurancePolicy(int id)
        {
            var policy = await _context.InsurancePolicies.FindAsync(id);
            if (policy != null)
            {
                _context.InsurancePolicies.Remove(policy);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<InsurancePolicy>> GetInsurancePoliciesByUserID(int userID)
        {
            return await _context.InsurancePolicies.Where(x => x.UserID == userID).ToListAsync();
        }
        public async Task<IEnumerable<InsurancePolicy>> GetInsurancePoliciesByName(string name)
        {
            return await _context.InsurancePolicies.Where(x => x.PolicyNumber.Contains(name)).ToListAsync();
        }
    }
}
