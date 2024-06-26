using AutoMapper;
using InsuranceWebAPI.Interfaces;
using InsuranceWebAPI.Models;
using InsuranceWebAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceWebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>List of users.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<UserDto>>), 200)]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<UserDto>>), 400)]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<UserDto>>), 404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ResponseDto<IEnumerable<UserDto>>>> GetUsers()
        {
            var responseUsers = await _userService.GetAllUsers();
            if (!responseUsers.IsSuccess)
            {
                return BadRequest(responseUsers);
            }

            if (responseUsers.Data == null || !responseUsers.Data.Any())
            {
                return NotFound(new ResponseDto<IEnumerable<UserDto>>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "No users found"
                });
            }

            var response = new ResponseDto<IEnumerable<UserDto>>
            {
                IsSuccess = true,
                Data = _mapper.Map<IEnumerable<UserDto>>(responseUsers.Data),
                Message = responseUsers.Message
            };

            return Ok(response);
        }

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>User details.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseDto<User>), 200)]
        [ProducesResponseType(typeof(ResponseDto<User>), 400)]
        [ProducesResponseType(typeof(ResponseDto<User>), 404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ResponseDto<User>>> GetUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ResponseDto<User> { IsSuccess = false, Message = "Invalid user ID" });
            }

            var response = await _userService.GetUserById(id);
            if (response.IsSuccess == false)
            {
                return BadRequest(response);
            }
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">User details.</param>
        /// <returns>The created user.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<UserDto>), 201)]
        [ProducesResponseType(typeof(ResponseDto<UserDto>), 400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ResponseDto<UserDto>>> CreateUser(User user)
        {
            var responseUser = await _userService.AddUser(user);
            ResponseDto<UserDto> response = new ResponseDto<UserDto>();
            response.IsSuccess = responseUser.IsSuccess;
            response.Message = responseUser.Message;
            response.Data = responseUser.Data == null ? null : _mapper.Map<UserDto>(responseUser.Data);

            if (!response.IsSuccess || response?.Data == null)
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(GetUser), new { id = response.Data.ID }, response);
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <param name="user">User details.</param>
        /// <returns>The updated user.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseDto<UserDto>), 200)]
        [ProducesResponseType(typeof(ResponseDto<UserDto>), 400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ResponseDto<UserDto>>> UpdateUser(int id, User user)
        {
            if (id != user.ID)
            {
                return BadRequest(new ResponseDto<UserDto> { IsSuccess = false, Message = "User ID mismatch" });
            }

            var responseUpdateUser = await _userService.UpdateUser(user);
            var response = new ResponseDto<UserDto>
            {
                IsSuccess = responseUpdateUser.IsSuccess,
                Message = responseUpdateUser.Message,
                Data = responseUpdateUser.Data != null ? _mapper.Map<UserDto>(responseUpdateUser.Data) : null
            };
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ResponseDto<bool>), 400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ResponseDto<bool>>> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
