using Microsoft.AspNetCore.Mvc;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.RequestDTO.RequestDTO.SwapRequestDTO;

namespace MSWT_API.Controllers
{
    [Route("api/shiftswaps")]
    [ApiController]
    public class ShiftSwapController : ControllerBase
    {
        private readonly IShiftSwapService _shiftSwapService;

        public ShiftSwapController(IShiftSwapService shiftSwapService)
        {
            _shiftSwapService = shiftSwapService;
        }
        // 1. Gửi yêu cầu hoán đổi
        [HttpPost("request")]
        public async Task<IActionResult> RequestSwap([FromBody] SwapRequestInput request)
        {
            var success = await _shiftSwapService.RequestSwapAsync(
                request.RequesterId,
                request.RequesterDate,
                request.TargetPhoneNumber,
                request.TargetDate);

            if (!success)
                return BadRequest("Gửi yêu cầu hoán đổi thất bại. Vui lòng kiểm tra dữ liệu đầu vào.");

            return Ok("Yêu cầu hoán đổi đã được gửi.");
        }

        // 2. Duyệt hoặc từ chối yêu cầu hoán đổi
        [HttpPost("respond")]
        public async Task<IActionResult> RespondSwap([FromBody] SwapRespondInput input)
        {
            var success = await _shiftSwapService.RespondToSwapAsync(
                input.RequestId,
                input.IsAccepted,
                input.Reason);

            if (!success)
                return BadRequest("Không thể xử lý yêu cầu hoán đổi. Vui lòng kiểm tra lại.");

            return Ok("Đã cập nhật trạng thái yêu cầu hoán đổi.");
        }

        // 3. Lấy danh sách yêu cầu đã gửi
        [HttpGet("my-requests/{userId}")]
        public async Task<IActionResult> GetMyRequests(string userId)
        {
            var requests = await _shiftSwapService.GetMySwapRequestsAsync(userId);
            return Ok(requests);
        }

    }
}
