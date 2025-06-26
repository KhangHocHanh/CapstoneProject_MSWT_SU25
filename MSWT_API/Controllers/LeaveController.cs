using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_BussinessObject;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using static MSWT_BussinessObject.Enum.Enum;
using MSWT_BussinessObject.Enum;

namespace MSWT_API.Controllers
{
    [Route("api/leaves")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _leaveService;
        private readonly IMapper _mapper;

        public LeaveController(ILeaveService leaveService, IMapper mapper)
        {
            _leaveService = leaveService;
            _mapper = mapper;
        }

        /// <summary>
        /// Nhân viên tạo đơn xin nghỉ phép
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Worker,Supervisor")]
        public async Task<IActionResult> CreateLeave([FromBody] LeaveCreateDto dto)
        {
            var userId = User.FindFirstValue("User_Id");
            if (userId == null) return Unauthorized();

            var leave = _mapper.Map<Leaf>(dto);
            leave.LeaveId = "LV" + DateTime.Now.Ticks;
            leave.WorkerId = userId;
            leave.RequestDate = DateOnly.FromDateTime(DateTime.Now);
            leave.TotalDays = (dto.EndDate.DayNumber - dto.StartDate.DayNumber) + 1;

            await _leaveService.AddLeave(leave);
            return Ok(new ResponseDTO(Const.SUCCESS_CREATE_CODE, "Tạo đơn nghỉ phép thành công", leave));
        }

        [HttpPatch("{id}/approval")]
        [Authorize(Roles = "Leader")]
        public async Task<IActionResult> ApproveOrRejectLeave(string id, [FromBody] LeaveApprovalDto dto)
        {
            var leave = await _leaveService.GetLeaveById(id);
            if (leave == null)
            {
                return NotFound(new ResponseDTO(Const.FAIL_READ_CODE, "Không tìm thấy đơn nghỉ phép."));
            }

            var currentUserId = User.FindFirstValue("User_Id");

            leave.ApprovalStatus = dto.ApprovalStatus.ToVietnamese();
            leave.ApprovalDate = DateOnly.FromDateTime(DateTime.Now);
            leave.ApprovedBy = currentUserId;
            leave.Note = dto.Note;

            await _leaveService.UpdateLeaf(leave);

            var message = dto.ApprovalStatus switch
            {
                ApprovalStatusEnum.DaDuyet => "✅ Đã duyệt đơn nghỉ phép.",
                ApprovalStatusEnum.TuChoi => "❌ Đơn nghỉ phép đã bị từ chối.",
                _ => "⚠️ Cập nhật trạng thái thành công."
            };

            return Ok(new ResponseDTO(Const.SUCCESS_UPDATE_CODE, message));
        }


        /// <summary>
        /// Lấy tất cả đơn nghỉ phép (Leader xem được tất cả, nhân viên chỉ thấy của mình)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetLeaves()
        {
            var userId = User.FindFirstValue("User_Id");
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            var leaves = await _leaveService.GetLeaves(userId, userRole);
            return Ok(new ResponseDTO(Const.SUCCESS_READ_CODE, "Danh sách đơn nghỉ phép", leaves));
        }

        /// <summary>
        /// Lấy chi tiết đơn nghỉ phép theo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaveById(string id)
        {
            var leave = await _leaveService.GetLeaveById(id);
            if (leave == null)
                return NotFound(new ResponseDTO(Const.FAIL_READ_CODE, "Không tìm thấy đơn nghỉ phép"));

            return Ok(new ResponseDTO(Const.SUCCESS_READ_CODE, "Lấy thông tin thành công", leave));
        }

        /// <summary>
        /// Xóa đơn nghỉ phép
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Leader")]
        public async Task<IActionResult> DeleteLeave(string id)
        {
            var success = await _leaveService.DeleteLeave(id);
            if (!success)
                return NotFound(new ResponseDTO(Const.FAIL_DELETE_CODE, "Xóa thất bại"));

            return Ok(new ResponseDTO(Const.SUCCESS_DELETE_CODE, "Xóa đơn nghỉ phép thành công"));
        }
        [HttpGet("my-leaves")]
        [Authorize]
        public async Task<IActionResult> GetMyLeaveHistory()
        {
            var userId = User.FindFirstValue("User_Id");
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Không thể xác định người dùng.");

            var leaves = await _leaveService.GetLeavesByUser(userId);
            return Ok(leaves);
        }

    }
}
