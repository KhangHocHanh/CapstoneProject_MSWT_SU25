using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_Services.IServices;

namespace MSWT_API.Controllers
{
    [Route("api/sensors")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _SensorService;

        public SensorController(ISensorService SensorService)
        {
            _SensorService = SensorService;
        }

        #region CRUD Category

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetAll()
        {
            return Ok(await _SensorService.GetAllSensors());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sensor>> GetById(string id)
        {
            var Sensor = await _SensorService.GetSensorById(id);
            if (Sensor == null)
                return NotFound();
            return Ok(Sensor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _SensorService.DeleteSensor(id);
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
