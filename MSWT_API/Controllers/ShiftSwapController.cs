using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_API.Controllers
{
    [Route("api/shiftswaps")]
    [ApiController]
    public class ShiftSwapController : ControllerBase
    {
        private readonly IShiftSwapService _service;

        public ShiftSwapController(IShiftSwapService shiftSwapService)
        {
            _service = shiftSwapService;

        }
        [HttpPost("request")]
        [Authorize]
        public async Task<IActionResult> RequestSwap([FromBody] ShiftSwapRequestDTO dto)
        {
            var requesterId = User.FindFirst("User_Id")?.Value;
            if (string.IsNullOrEmpty(requesterId)) return Unauthorized();

            var result = await _service.RequestSwapAsync(requesterId, dto);
            return Ok(result);
        }

        [HttpGet("my-requests")]
        [Authorize]
        public async Task<IActionResult> GetMyRequests()
        {
            var userId = User.FindFirst("User_Id")?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _service.GetUserRequestsAsync(userId);
            return Ok(result);
        }

        [HttpPost("respond")]
        [Authorize]
        public async Task<IActionResult> RespondSwap([FromBody] ShiftSwapRespondDTO dto)
        {
            var userId = User.FindFirst("User_Id")?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _service.RespondSwapAsync(userId, dto);
            if (result == null) return NotFound("Request not found or not allowed to respond.");

            return Ok(result);
        }
    }
}