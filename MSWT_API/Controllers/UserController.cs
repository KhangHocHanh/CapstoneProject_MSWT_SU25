using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject;
using MSWT_BussinessObject.Enum;
using MSWT_BussinessObject.Model;
using MSWT_Services;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.Enum.Enum;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;

namespace MSWT_API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        #region CRUD User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return Ok(await _userService.GetAllUserWithRoleAsync());
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "admin, staff")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                    return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = _mapper.Map<User>(dto);
            newUser.UserId = "US" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            newUser.CreateAt = DateOnly.FromDateTime(TimeHelper.GetNowInVietnamTime());
            newUser.Status = UserStatusHelper.ToStringStatus(UserStatusEnum.ChuaXacThuc);

            await _userService.AddUser(newUser);
            return Ok(new { message = "Tạo người dùng thành công", newUser.UserId });
        }

        #endregion

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO userDto)
        {
            try
            {
                var response = await _userService.Login(userDto);

                if (response.Status == Const.FAIL_READ_CODE)
                {
                    return Unauthorized(response.Message);
                }

                if (response.Data == null) // Check if the token is missing
                {
                    return StatusCode(500, new { message = "Login failed due to internal error." });
                }
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                // return BadRequest(ex.Message);
                return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
            }
        }
        [HttpPut("update-profile")]
        [Authorize] // Ai cũng có thể gọi
        public async Task<IActionResult> UpdateProfile([FromBody] UserUpdateProfileDto dto)
        {
            var userId = User.FindFirstValue("User_Id");
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Không xác định được người dùng.");

            var result = await _userService.UpdateUserProfile(userId, dto);
            if (result.Status != Const.SUCCESS_UPDATE_CODE)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("update-status/{id}")]
        [Authorize(Roles = "Leader")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] UserStatusUpdateDto dto)
        {
            var result = await _userService.UpdateUserStatusToQuit(id, dto.Note);
            if (result.Status != Const.SUCCESS_UPDATE_CODE)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpPost("logout")]
            public async Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync(); 
                return Ok(new { message = "Logout successful." });
            }

        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var userId = User.FindFirstValue("User_Id");
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Không xác định được người dùng.");

            var result = await _userService.ChangePasswordAsync(userId, dto);
            if (result.Status != Const.SUCCESS_UPDATE_CODE)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPut("change-password-by-phoneNumber")]
        public async Task<IActionResult> ChangePasswordByPhoneNumber([FromBody] ChangePasswordByPhoneNumberDto dto)
        {

            var result = await _userService.ChangePasswordByPhoneNumberAsync(dto);
            if (result.Status != Const.SUCCESS_UPDATE_CODE)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}/avatar")]
        public async Task<IActionResult> UpdateAccountAvatar(string id, IFormFile avatarFile)
        {
                var result = await _userService.UpdateAvatarUrl(id, avatarFile);
                return Ok(result);   
        }

        [HttpGet("unassigned-workers")]
        public async Task<IActionResult> GetUnassignedWorkers()
        {
            var users = await _userService.GetUnassignedWorkersAsync();
            return Ok(users);
        }

        [HttpGet("unassigned-supervisors")]
        public async Task<IActionResult> GetUnassignedSupervisors()
        {
            var users = await _userService.GetUnassignedSupervisorsAsync();
            return Ok(users);
        }
    }
}
