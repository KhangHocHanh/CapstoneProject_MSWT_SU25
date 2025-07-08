using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Enum;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Services.IServices;
using MSWT_Services.Services;
using static MSWT_BussinessObject.Enum.Enum;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;

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

        [HttpPost]
        public async Task<ActionResult<Sensor>> Create([FromBody] SensorCreateDto dto)
        {
            var newSensor = new Sensor
            {
                SensorId = "SN" + DateTime.UtcNow.Ticks,
                SensorName = dto.SensorName,
                Status = SensorStatus.DangHoatDong.ToDisplayString()  // "Đang hoạt động"
            };

            await _SensorService.AddSensor(newSensor);

            return CreatedAtAction(nameof(GetById),
                new { id = newSensor.SensorId }, newSensor);
        }

        [HttpPut("toggle-status/{id}")]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            var sensor = await _SensorService.GetSensorById(id);
            if (sensor == null)
                return NotFound(new { message = "Cảm biến không tồn tại" });

            // Đảo trạng thái
            if (sensor.Status == RoleStatus.DangHoatDong.ToDisplayString())
                sensor.Status = RoleStatus.NgungHoatDong.ToDisplayString();
            else
                sensor.Status = RoleStatus.DangHoatDong.ToDisplayString();

            await _SensorService.UpdateSensor(sensor);

            return Ok(new
            {
                message = "Cập nhật trạng thái thành công",
                newStatus = sensor.Status
            });
        }
        #endregion

    }
}
