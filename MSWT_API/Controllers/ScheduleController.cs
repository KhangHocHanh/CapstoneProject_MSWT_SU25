using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_Services.IServices;

namespace MSWT_API.Controllers
{
    [Route("api/schedules")]
    [ApiController]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        #region CRUD Category

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAll()
        {
            return Ok(await _scheduleService.GetAllSchedule());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetById(string id)
        {
            var schedule = await _scheduleService.GetScheduleById(id);
            if (schedule == null)
                return NotFound();
            return Ok(schedule);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _scheduleService.DeleteSchedule(id);
                return NoContent(); // 204 No Content if success
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 Bad Request if error
            }
        }
        #endregion

    }
}
