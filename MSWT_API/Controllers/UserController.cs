using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject;
using MSWT_BussinessObject.Model;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;

namespace MSWT_API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #region CRUD User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return Ok(await _userService.GetAllUsers());
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

        //[HttpPost]
        //public async Task<ActionResult> Register([FromBody] UserRegisterDTO userDto)
        //{
        //    try
        //    {
        //        await _userService.AddUser(userDto);
        //        return Ok("User registered successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(int id, [FromBody] UserDTO userDto)
        //{
        //    await _userService.UpdateUser(id, userDto);
        //    return NoContent();
        //}

        //[Authorize(Roles = "admin, staff")]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    await _userService.DeleteUser(id);
        //    return NoContent();
        //}
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

            [HttpPost("logout")]
            public async Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync(); 
                return Ok(new { message = "Logout successful." });
            }
        
    }
}
