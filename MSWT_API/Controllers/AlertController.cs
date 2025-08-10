using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Services.IServices;
using MSWT_Services.Services;
using static MSWT_BussinessObject.Enum.Enum;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;
using MSWT_BussinessObject.Enum;
using MSWT_Services;

namespace MSWT_API.Controllers
{
    [Route("api/alerts")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly IAlertService _alertService;
        public AlertController(IAlertService alertService)
        {
            _alertService = alertService;
        }


        #region CRUD Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseDTO>>> GetAll()
        {
            return Ok(await _alertService.GetAllAlerts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetById(string id)
        {
            var alert = await _alertService.GetAlertById(id);
            if (alert == null)
                return NotFound();
            return Ok(alert);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _alertService.DeleteAlert(id);
                return NoContent(); // 204 No Content if success
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 Bad Request if error
            }
        }
        [HttpPost]
        public async Task<ActionResult<Alert>> Create([FromBody] AlertRequestDTO dto)
        {
            // Kiểm tra xem đã có cảnh báo chưa xử lý cho thùng rác này chưa
            var existingAlert = await _alertService
                .GetAlertByTrashBinIdAsync(dto.TrashBinId, AlertStatus.ChuaXuLy.ToDisplayString());

            if (existingAlert != null)
            {
                // Đã có thông báo cần xử lý => Không tạo mới
                return Conflict(new { message = "Thùng rác này đã có thông báo cần xử lý." });
            }

            var newAlert = new Alert
            {
                AlertId = "AL" + DateTime.UtcNow.Ticks,
                TrashBinId = dto.TrashBinId,
                TimeSend = TimeHelper.GetNowInVietnamTime(),
                Status = AlertStatus.ChuaXuLy.ToDisplayString(),
                UserId = null
            };

            await _alertService.CreateAlertAsync(newAlert);

            return CreatedAtAction(nameof(GetById),
                new { id = newAlert.AlertId }, newAlert);
        }
        [HttpGet("my-alerts")]
        [Authorize]
        public async Task<IActionResult> GetMyAlertsHistory()
        {
            var userId = User.FindFirstValue("User_Id");
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Không thể xác định người dùng.");

            var leaves = await _alertService.GetAllAlertsByUserIdAsync(userId);
            return Ok(leaves);
        }
        [HttpPut("{alertId}/resolve")]
        [Authorize]
        public async Task<IActionResult> UpdateAlertStatus(string alertId)
        {
            var userId = User.FindFirstValue("User_Id");
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Không thể xác định người dùng.");
            try
            {
                await _alertService.UpdateAlertStatusAsync(alertId);
                return Ok(new { message = "Cập nhật trạng thái cảnh báo thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        #endregion
    }
}
