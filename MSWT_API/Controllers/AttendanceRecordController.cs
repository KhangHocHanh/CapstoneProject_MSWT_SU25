using Microsoft.AspNetCore.Mvc;
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
            var records = await _attendanceService.GetAllAttendanceRecordsAsync();
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

    }
}
