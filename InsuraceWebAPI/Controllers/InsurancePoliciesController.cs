using AutoMapper;
using InsuranceWebAPI.Interfaces;
using InsuranceWebAPI.Models.Dto;
using InsuranceWebAPI.Data.Repositories;
using InsuranceWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceWebAPI.Controllers
{
    [Route("api/insurancepolicies")]
    [ApiController]
    public class InsurancePoliciesController : ControllerBase
    {
        private readonly IInsurancePolicyService _policyService;
        private readonly IUserService _userService;

        public InsurancePoliciesController(IInsurancePolicyService policyService, IUserService userService, IMapper mapper)
        {
            _policyService = policyService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsurancePolicyDto>>> GetInsurancePolicies()
        {
            var response = await _policyService.GetAllInsurancePolicies();
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InsurancePolicyDto>> GetInsurancePolicy(int id)
        {
            var response = await _policyService.GetInsurancePolicyById(id);
            if (!response.IsSuccess)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<InsurancePolicyDto>> CreateInsurancePolicy(InsurancePolicyDto policyDto)
        {
            var userResponse = await _userService.GetUserById(policyDto.UserID);
            if (!userResponse.IsSuccess || userResponse.Data == null)
            {
                return BadRequest(userResponse);
            }

            var response = await _policyService.AddInsurancePolicy(policyDto);
            if (!response.IsSuccess || response.Data == null)
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(GetInsurancePolicy), new { id = response.Data.ID }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInsurancePolicy(int id, InsurancePolicyDto pol)
        {
            if (id != pol.ID)
            {
                return BadRequest();
            }

            var existingPolicyResponse = await _policyService.GetInsurancePolicyById(id);
            if (!existingPolicyResponse.IsSuccess || existingPolicyResponse.Data == null)
            {
                return NotFound(existingPolicyResponse);
            }
            var response = await _policyService.UpdateInsurancePolicy(pol);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsurancePolicy(int id)
        {
            var response = await _policyService.DeleteInsurancePolicy(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
