using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Enum;
using MSWT_BussinessObject.Model;
using MSWT_Services.IServices;
using MSWT_Services.Services;
using static MSWT_BussinessObject.Enum.Enum;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_API.Controllers
{
    [Route("api/sensorBins")]
    [ApiController]
    public class SensorBinController : ControllerBase
    {
        private readonly ISensorBinService _sensorBinService;
        private readonly ISensorService _SensorService;
        private readonly ITrashBinService _TrashBinService;

        public SensorBinController(ISensorBinService sensorBinService, ISensorService sensorService, ITrashBinService trashBinService)
        {
            _sensorBinService = sensorBinService;
            _SensorService = sensorService;
            _TrashBinService = trashBinService;
        }
        #region CRUD Category

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorBin>>> GetAll()
        {
            return Ok(await _sensorBinService.GetAllSensorBins());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SensorBin>> GetById(string id)
        {
            var sensorBin = await _sensorBinService.GetSensorBinById(id);
            if (sensorBin == null)
                return NotFound();
            return Ok(sensorBin);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _sensorBinService.DeleteSensorBin(id);
                return NoContent(); // 204 No Content if success
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 Bad Request if error
            }
        }

        [HttpPost]
        public async Task<ActionResult<SensorBin>> Create([FromBody] SensorBinCreateDto dto)
        {
            var newSensorBin = new SensorBin
            {
                SensorId = dto.SensorId,
                BinId = dto.BinId,
                Status = SensorStatus.DangHoatDong.ToDisplayString()  // "Đang hoạt động"
            };
            await _sensorBinService.AddSensorBin(newSensorBin);
            return CreatedAtAction(nameof(GetById), new { id = newSensorBin.SensorId }, newSensorBin);
        }

        [HttpPut("toggle-status/{id}")]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            var sensorBin = await _sensorBinService.GetSensorBinById(id);
            if (sensorBin == null)
                return NotFound(new { message = "Không tồn tại" });

            // Đảo trạng thái
            if (sensorBin.Status == RoleStatus.DangHoatDong.ToDisplayString())
                sensorBin.Status = RoleStatus.NgungHoatDong.ToDisplayString();
            else
                sensorBin.Status = RoleStatus.DangHoatDong.ToDisplayString();

            await _sensorBinService.UpdateSensorBin(sensorBin);

            return Ok(new
            {
                message = "Cập nhật trạng thái thành công",
                newStatus = sensorBin.Status
            });
        }
        [HttpPost("measure")]
        public async Task<IActionResult> MeasureFillLevel([FromBody] SensorMeasurementDto dto)
        {
            var sensorBin = await _sensorBinService.GetSensorBinById(dto.SensorId);
            if (sensorBin == null)
                return NotFound(new { message = "Sensor không tồn tại" });

            sensorBin.FillLevel = dto.FillLevel;
            sensorBin.MeasuredAt = DateTime.UtcNow;
            await _sensorBinService.UpdateSensorBin(sensorBin);

            return Ok(new
            {
                message = "Mức độ thùng rác đã được cập nhật",
                currentFill = sensorBin.FillLevel
            });
        }
        #endregion


    }
}
