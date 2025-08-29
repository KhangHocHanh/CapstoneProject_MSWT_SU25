using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_Services.IServices;
using MSWT_Services.Services;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;

namespace MSWT_API.Controllers
{
    [Route("api/scheduledetails")]
    [ApiController]
    public class ScheduleDetailsController : Controller
    {
        private readonly IScheduleDetailsService _scheduleDetailsService;

        public ScheduleDetailsController(IScheduleDetailsService scheduleDetailsService)
        {
            _scheduleDetailsService = scheduleDetailsService;
        }

        #region CRUD Category

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDetailsResponseDTO>>> GetAll()
        {
            return Ok(await _scheduleDetailsService.GetAllSchedule());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleDetail>> GetById(string id)
        {
            var schedule = await _scheduleDetailsService.GetScheduleById(id);
            if (schedule == null)
                return NotFound();
            return Ok(schedule);
        }

        //[HttpGet("user/{userId}")]
        //public async Task<IActionResult> SearchScheduleDetailsByUserId(string userId)
        //{
        //    var results = await _scheduleDetailsService.SearchScheduleDetailsByUserIdAsync(userId);
        //    return Ok(results);
        //}


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _scheduleDetailsService.DeleteSchedule(id);
                return NoContent(); // 204 No Content if success
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 Bad Request if error
            }
        }
        #endregion
        [HttpPost("{scheduleId}/details")]
        public async Task<IActionResult> CreateScheduleDetail(string scheduleId, [FromBody] ScheduleDetailsRequestDTO detailDto)
        {
            var result = await _scheduleDetailsService.CreateScheduleDetailFromScheduleAsync(scheduleId, detailDto);
            return Ok(result);
        }

        [HttpGet("scheduleDetails/{userId}")]
        public async Task<IActionResult> GetSchedulesByUserId(string userId)
        {
            try
            {
                var schedules = await _scheduleDetailsService.GetSchedulesByUserIdAsync(userId);
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving schedules for user: {ex.Message}");
            }
        }

        [HttpGet("by-user-date")]
        public async Task<IActionResult> GetByUserIdAndDate(string userId, DateTime date)
        {
            var results = await _scheduleDetailsService.GetByUserIdAndDateAsync(userId, date);
            return Ok(results);
        }

        [HttpGet("by-date-paginated")]
        public async Task<IActionResult> GetByDatePaginated(DateTime date, int pageNumber = 1, int pageSize = 20)
        {
            var results = await _scheduleDetailsService.GetByDatePaginatedAsync(date, pageNumber, pageSize);
            return Ok(results);
        }


        //[HttpPut("{id}/assignments/{assignmentId}")]
        //public async Task<IActionResult> AddAssignmentToSchedule(string id, string assignmentId)
        //{
        //    try
        //    {
        //        var success = await _scheduleDetailsService.AddAssignmentToSchedule(id, assignmentId);
        //        if (success)
        //            return Ok(new { message = "Assignment added to schedule detail successfully." });

        //        return BadRequest("Could not assign the assignment.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { error = ex.Message });
        //    }
        //}


        //[HttpPut("user/worker/{id}")]
        //public async Task<IActionResult> AssignWorker(string id, [FromBody] string userId)
        //{
        //    try
        //    {
        //        var result = await _scheduleDetailsService.AddWorkerToSchedule(id, userId);
        //        if (!result)
        //            return BadRequest(new { message = "Failed to assign worker to schedule." });

        //        return Ok(new { message = "Worker assigned successfully." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpPut("user/supervisor/{id}")]
        //public async Task<IActionResult> AssignSupervisor(string id, [FromBody] string userId)
        //{
        //    try
        //    {
        //        var result = await _scheduleDetailsService.AddSupervisorToSchedule(id, userId);
        //        if (!result)
        //            return BadRequest(new { message = "Failed to assign supervisor to schedule." });

        //        return Ok(new { message = "Supervisor assigned successfully." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        [HttpPut("scheduledetails/rating/{id}")]
        public async Task<IActionResult> UpdateRatingAndClose(string id, [FromBody] ScheduleDetailsUpdateRatingRequestDTO request)
        {
            var result = await _scheduleDetailsService.UpdateScheduleDetailRatingAsync(id, request);
            return Ok(result);
        }

        [HttpPut("scheduledetails/status/{id}")]
        public async Task<IActionResult> MarkScheduleAsCompleted(string id)
        {
            var result = await _scheduleDetailsService.MarkAsComplete(id);
            return Ok(result);
        }

        //[HttpPost("schedule-detail/rating")]
        //[Authorize(Roles = "Supervisor")] // hoặc bỏ nếu mọi role đều có quyền
        //public async Task<IActionResult> CreateDailyRating([FromBody] ScheduleDetailRatingCreateDTO dto)
        //{
        //    var userId = User.FindFirstValue("User_Id");
        //    if (userId == null) return Unauthorized();

        //    try
        //    {
        //        await _scheduleDetailsService.CreateDailyRatingAsync(userId, dto);
        //        return Ok(new { message = "Rating submitted." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpPut("schedule-details/{id}/status")]
        //[Authorize(Roles = "Worker")]
        //public async Task<IActionResult> MarkScheduleDetailAsComplete(string id, IFormFile? evidenceImage)
        //{
        //    try
        //    {
        //        var userId = User.FindFirst("User_Id")?.Value;
        //        if (string.IsNullOrEmpty(userId))
        //            return Unauthorized("Không thể xác định người dùng.");

        //        var result = await _scheduleDetailsService.UpdateScheduleDetailStatusToComplete(id, userId, evidenceImage);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpGet("average-rating")]
        //public async Task<IActionResult> GetAverageRating([FromQuery] int year, [FromQuery] int month)
        //{
        //    try
        //    {
        //        var userId = User.FindFirst("User_Id")?.Value;
        //        if (string.IsNullOrEmpty(userId))
        //            return Unauthorized("Không thể xác định người dùng.");

        //        var result = await _scheduleDetailsService.GetAverageRatingForMonthAsync(userId,year,month);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpGet("my-work-stats")]
        //[Authorize]
        //public async Task<IActionResult> GetMyWorkStats([FromQuery] int month, [FromQuery] int year)
        //{
        //    var userId = User.FindFirstValue("User_Id");
        //    if (string.IsNullOrEmpty(userId))
        //        return Unauthorized("Không thể xác định người dùng.");

        //    var (workedDays, totalDays, percentage) = await _scheduleDetailsService.GetWorkStatsInMonthAsync(userId, month, year);

        //    return Ok(new
        //    {
        //        Month = month,
        //        Year = year,
        //        WorkedDays = workedDays,
        //        TotalDays = totalDays,
        //        Percentage = Math.Round(percentage, 2)
        //    });
        //}
    }
}
