using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_Services.IServices;

namespace MSWT_API.Controllers
{
    [Route("api/shifts")]
    [ApiController]
    public class ShiftController : Controller
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        #region CRUD Category

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shift>>> GetAll()
        {
            return Ok(await _shiftService.GetAllShifts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Shift>> GetById(string id)
        {
            var shift = await _shiftService.GetShiftById(id);
            if (shift == null)
                return NotFound();
            return Ok(shift);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _shiftService.DeleteShift(id);
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
