using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.Enum.Enum;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using AutoMapper;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_BussinessObject;
using MSWT_BussinessObject.Enum;

namespace MSWT_API.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public ReportController(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }

        // GET api/report
        /// <summary>
        /// Lấy toàn bộ báo cáo.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetAll()
        {
            var reports = await _reportService.GetAllReports();
            return Ok(reports);
        }

        // GET api/report/{id}
        /// <summary>
        /// Lấy chi tiết báo cáo theo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetById(string id)
        {
            var report = await _reportService.GetReportById(id);
            if (report == null)
                return NotFound();

            return Ok(report);
        }

        // POST api/report (chỉ cho Nhân viên - Báo cáo sự cố)
        [HttpPost]
        [Authorize(Roles = "Worker,Supervisor")]
        public async Task<IActionResult> CreateIncidentReport([FromBody] ReportCreateDto dto)
        {
            var userId = User.FindFirstValue("User_Id");
            if (userId == null) return Unauthorized();

            var report = _mapper.Map<Report>(dto);
            report.ReportId = $"RP{DateTime.Now:yyyyMMddHHmmss}";
            report.UserId = userId;
            report.ReportType = ReportTypeEnum.SuCo.ToVietnamese(); // Gán cố định

            await _reportService.AddReport(report);
            return CreatedAtAction(nameof(GetById), new { id = report.ReportId }, report);
        }

        // POST api/report/leader (chỉ cho Leader)
        [HttpPost("leader")]
        [Authorize(Roles = "Leader")]
        public async Task<IActionResult> CreateLeaderReport([FromBody] ReportCreateDtoWithType dto)
        {
            var userId = User.FindFirstValue("User_Id");
            if (userId == null) return Unauthorized();

            var report = _mapper.Map<Report>(dto);
            report.ReportId = $"RP{DateTime.Now:yyyyMMddHHmmss}";
            report.UserId = userId;

            await _reportService.AddReport(report);
            return CreatedAtAction(nameof(GetById), new { id = report.ReportId }, report);
        }

        // PUT api/report/{id}
        /// <summary>
        /// Cập nhật báo cáo.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Report report)
        {
            if (id != report.ReportId)
                return BadRequest("Không tìm thấy Report tương ứng");

            var existing = await _reportService.GetReportById(id);
            if (existing == null)
                return NotFound();

            await _reportService.UpdateReport(report);
            return NoContent(); // 204
        }

        // DELETE api/report/{id}
        /// <summary>
        /// Xoá báo cáo.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _reportService.GetReportById(id);
            if (existing == null)
                return NotFound();

            await _reportService.DeleteReport(id);
            return NoContent(); // 204
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Leader")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] ReportUpdateStatusDto dto)
        {
            var report = await _reportService.GetReportById(id);
            if (report == null) return NotFound("Không tìm thấy báo cáo.");

            var currentStatus = ReportStatusHelper.ToEnum(report.Status ?? "");
            var newStatus = dto.NewStatus;

            if (currentStatus == ReportStatus.DaHoanThanh)
                return BadRequest("Báo cáo đã hoàn thành. Không thể cập nhật thêm.");

            if (!ReportStatusHelper.CanUpdateStatus(currentStatus, newStatus))
                return BadRequest($"Không thể chuyển trạng thái từ '{report.Status}' sang '{newStatus.ToVietnamese()}'.");

            report.Status = newStatus.ToVietnamese();
            await _reportService.UpdateReport(report);

            return Ok("Cập nhật trạng thái thành công.");
        }
    }
}
