using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using AutoMapper;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_BussinessObject;
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

        #region CRUD Category

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetAll()
        {
            return Ok(await _leaveService.GetAllLeafs());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sensor>> GetById(string id)
        {
            var Sensor = await _leaveService.GetLeafById(id);
            if (Sensor == null)
                return NotFound();
            return Ok(Sensor);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] LeaveCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.EndDate < dto.StartDate)
                return BadRequest("Ngày kết thúc phải lớn hơn ngày bắt đầu.");

            var workerId = User.FindFirstValue("User_Id");
            if (string.IsNullOrEmpty(workerId))
                return Unauthorized("Không tìm thấy thông tin đăng nhập.");

            int totalDays = (dto.EndDate.ToDateTime(TimeOnly.MinValue) - dto.StartDate.ToDateTime(TimeOnly.MinValue)).Days + 1;

            var leaf = _mapper.Map<Leaf>(dto);
            leaf.LeaveId = "LE" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            leaf.WorkerId = workerId;
            leaf.TotalDays = totalDays;
            leaf.RequestDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await _leaveService.AddLeaf(leaf);

            var response = new ResponseDTO(
          Const.SUCCESS_CREATE_CODE,
          Const.SUCCESS_CREATE_MSG,
          null
      );

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _leaveService.DeleteLeaf(id);
                return NoContent(); // 204 No Content if success
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 Bad Request if error
            }
        }

        [HttpPut("approve/{id}")]
        [Authorize(Roles = "Leader")]
        public async Task<IActionResult> ApproveLeave(string id, [FromBody] LeaveApprovalDto dto)
        {
            var leaf = await _leaveService.GetLeafById(id);
            if (leaf == null)
                return NotFound("Không tìm thấy đơn nghỉ.");

            if (leaf.ApprovalStatus != "Chưa duyệt")
                return BadRequest("Đơn nghỉ đã được xử lý.");

            var approverId = User.FindFirstValue("User_Id");
            if (string.IsNullOrEmpty(approverId))
                return Unauthorized("Không tìm thấy thông tin người duyệt.");

            // Cập nhật trạng thái đơn nghỉ
            leaf.ApprovalStatus = dto.ApprovalStatus.ToVietnamese();
            leaf.ApprovedBy = approverId;
            leaf.ApprovalDate = DateOnly.FromDateTime(DateTime.UtcNow);
            leaf.Note = dto.Note;

            await _leaveService.UpdateLeaf(leaf);

            return Ok(new { message = "Đã cập nhật đơn nghỉ thành công." });
        }
        #endregion
    }
}
