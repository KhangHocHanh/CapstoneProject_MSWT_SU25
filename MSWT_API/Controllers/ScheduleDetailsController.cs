using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_Services.IServices;
using MSWT_Services.Services;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;

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
        public async Task<ActionResult<IEnumerable<ScheduleDetail>>> GetAll()
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

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> SearchScheduleDetailsByUserId(string userId)
        {
            var results = await _scheduleDetailsService.SearchScheduleDetailsByUserIdAsync(userId);
            return Ok(results);
        }


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

        [HttpPut("{id}/assignments/{assignmentId}")]
        public async Task<IActionResult> AddAssignmentToSchedule(string id, string assignmentId)
        {
            try
            {
                var success = await _scheduleDetailsService.AddAssignmentToSchedule(id, assignmentId);
                if (success)
                    return Ok(new { message = "Assignment added to schedule detail successfully." });

                return BadRequest("Could not assign the assignment.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPut("user/worker/{id}")]
        public async Task<IActionResult> AssignWorker(string id, [FromBody] string userId)
        {
            try
            {
                var result = await _scheduleDetailsService.AddWorkerToSchedule(id, userId);
                if (!result)
                    return BadRequest(new { message = "Failed to assign worker to schedule." });

                return Ok(new { message = "Worker assigned successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("user/supervisor/{id}")]
        public async Task<IActionResult> AssignSupervisor(string id, [FromBody] string userId)
        {
            try
            {
                var result = await _scheduleDetailsService.AddSupervisorToSchedule(id, userId);
                if (!result)
                    return BadRequest(new { message = "Failed to assign supervisor to schedule." });

                return Ok(new { message = "Supervisor assigned successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("scheduledetails/rating/{id}")]
        public async Task<IActionResult> UpdateRating(string id, [FromBody] string rating)
        {
            try
            {
                var result = await _scheduleDetailsService.UpdateRating(id, rating);
                if (!result)
                    return BadRequest(new { message = "Failed to rate scheduleDetails." });

                return Ok(new { message = "scheduleDetails rated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("schedule-detail/rating")]
        [Authorize(Roles = "Supervisor")] // hoặc bỏ nếu mọi role đều có quyền
        public async Task<IActionResult> CreateDailyRating([FromBody] ScheduleDetailRatingCreateDTO dto)
        {
            var userId = User.FindFirstValue("User_Id");
            if (userId == null) return Unauthorized();

            try
            {
                await _scheduleDetailsService.CreateDailyRatingAsync(userId, dto);
                return Ok(new { message = "Rating submitted." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
