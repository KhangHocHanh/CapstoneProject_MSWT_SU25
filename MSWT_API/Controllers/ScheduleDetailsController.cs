using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_Services.IServices;

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

    }
}
