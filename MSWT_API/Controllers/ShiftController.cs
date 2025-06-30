using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
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
        [HttpPost]
        public async Task<ActionResult<ShiftResponseDTO>> AddShift([FromBody] ShiftRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _shiftService.AddShift(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

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

        [HttpPut("{id}")]
        public async Task<ActionResult<ShiftResponseDTO>> UpdateShift(string id, [FromBody] ShiftRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _shiftService.UpdateShift(id, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
