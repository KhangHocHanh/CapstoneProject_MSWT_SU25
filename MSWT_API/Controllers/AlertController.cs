using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Services.IServices;
using MSWT_Services.Services;

namespace MSWT_API.Controllers
{
    [Route("api/alerts")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly IAlertService _alertService;
        public AlertController(IAlertService alertService)
        {
            _alertService = alertService;
        }


        #region CRUD Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseDTO>>> GetAll()
        {
            return Ok(await _alertService.GetAllAlerts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetById(string id)
        {
            var alert = await _alertService.GetAlertById(id);
            if (alert == null)
                return NotFound();
            return Ok(alert);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _alertService.DeleteAlert(id);
                return NoContent(); // 204 No Content if success
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 Bad Request if error
            }
        }
        //#region CRUD Category
        //[HttpPost]
        //public async Task<ActionResult<FloorResponseDTO>> CreateFloor([FromBody] FloorRequestDTO request)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var createdFloor = await _floorService.CreateFloorAsync(request);
        //    return Ok(createdFloor);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateFloor(string id, [FromBody] FloorRequestDTO request)
        //{
        //    try
        //    {
        //        var result = await _floorService.UpdateFloor(id, request);
        //        if (!result)
        //        {
        //            return NotFound($"Floor with ID {id} not found.");
        //        }

        //        return Ok(); // or Ok() if you prefer returning a success message
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

#endregion
    }
}
