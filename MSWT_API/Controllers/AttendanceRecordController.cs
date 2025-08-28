using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;

namespace MSWT_API.Controllers
{
    [ApiController]
    [Route("api/attendanceRecord")]
    public class AttendanceRecordController : ControllerBase
    {
        private readonly IAttendanceRecordService _attendanceService;

        public AttendanceRecordController(IAttendanceRecordService service)
        {
            _attendanceService = service;
        }
        [HttpPost("monthly")]
        public async Task<IActionResult> CreateMonthlyAttendanceRecords([FromBody] CreateMonthlyAttendanceRequest request)
        {
            var result = await _attendanceService.CreateMonthlyAttendanceRecordsAsync(request);
            if (!result.IsSuccess)
                return BadRequest(result.Message);
            return Ok(result.Message);
        }
        [HttpGet("my-records")]
        public async Task<IActionResult> GetMyAttendanceRecords()
        {
            string userId = User.FindFirst("User_Id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Không xác định được người dùng.");

            var records = await _attendanceService.GetAttendanceRecordsByUserAsync(userId);
            return Ok(records);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAttendanceRecords()
        {
            var records = await _attendanceService.GetAllAttendanceRecordWithFullName();
            return Ok(records);
        }
        [HttpGet("date/{date}")]
        public async Task<IActionResult> GetRecordsByDate(DateOnly date)
        {
            var records = await _attendanceService.GetRecordsByDateAsync(date);
            return Ok(records);
        }


        [HttpPost("checkin")]
        public async Task<IActionResult> CheckInAsync()
        {
            string userId = User.FindFirst("User_Id")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Không xác định được người dùng đăng nhập.");

            var result = await _attendanceService.CheckInAsync(userId);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOutAsync()
        {
            string userId = User.FindFirst("User_Id")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Không xác định được người dùng đăng nhập.");

            var result = await _attendanceService.CheckOutAsync(userId);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
        [HttpPost("add-new-employee")]
        public async Task<IActionResult> AddNewEmployeeAttendance([FromBody] AddNewEmployeeAttendanceRequest request)
        {
            var result = await _attendanceService.AddNewEmployeeAttendanceAsync(request);
            if (!result.IsSuccess)
                return BadRequest(result.Message);
            return Ok(result.Message);
        }
        [HttpGet("export")]
        public async Task<IActionResult> ExportMonthlyAttendance([FromQuery] int year, [FromQuery] int month)
        {
            var fileBytes = await _attendanceService.ExportMonthlyAttendanceAsync(year, month);
            var fileName = $"BaoCaoDiemDanh_{month:D2}_{year}.xlsx";
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        // Lấy danh sách nhân viên trong nhóm supervisor
        [HttpGet("employees")]
        public async Task<IActionResult> GetEmployees()
        {
            // Lấy UserId từ JWT Claims
            var supervisorId = User.FindFirst("User_Id")?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(supervisorId) || role != "Supervisor")
                return Unauthorized("Bạn không có quyền xem danh sách này");

            var employees = await _attendanceService.GetEmployeesAsync(supervisorId);
            return Ok(employees);
        }

        // POST: Supervisor điểm danh
        [HttpPost("save")]
        public async Task<IActionResult> SaveAttendances([FromBody] SaveAttendanceRequest request)
        {
            var supervisorId = User.FindFirst("User_Id")?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(supervisorId) || role != "Supervisor")
                return Unauthorized("Bạn không có quyền điểm danh");

            var success = await _attendanceService.SaveAttendancesAsync(supervisorId, request.PresentEmployeeIds, request.Shift);

            if (!success) return BadRequest("Không thể lưu điểm danh");

            return Ok(new { message = "Điểm danh thành công" });
        }
    }

    public class SaveAttendanceRequest
    {
        public List<string> PresentEmployeeIds { get; set; } = new();
        public string Shift { get; set; } = null!; // "Morning" hoặc "Afternoon"
    }


}
