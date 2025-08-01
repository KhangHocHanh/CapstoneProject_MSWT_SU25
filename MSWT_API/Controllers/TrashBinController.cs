using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Enum;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Services.IServices;
using MSWT_Services.Services;
using static MSWT_BussinessObject.Enum.Enum;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_API.Controllers
{
    [Route("api/trashbins")]
    [ApiController]
    public class TrashBinController : ControllerBase
    {
        private readonly ITrashBinService _TrashBinService;

        public TrashBinController(ITrashBinService TrashBinService)
        {
            _TrashBinService = TrashBinService;
        }

        #region CRUD Category

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrashBin>>> GetAll()
        {
            return Ok(await _TrashBinService.GetAllTrashBins());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrashBin>> GetById(string id)
        {
            var TrashBin = await _TrashBinService.GetTrashBinById(id);
            if (TrashBin == null)
                return NotFound();
            return Ok(TrashBin);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _TrashBinService.DeleteTrashBin(id);
                return NoContent(); // 204 No Content if success
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 Bad Request if error
            }
        }

        [HttpPost]
        public async Task<ActionResult<TrashBin>> Create([FromBody] TrashbinCreateDto dto)
        {
            var newTrashbin = new TrashBin
            {
                TrashBinId = "TB" + DateTime.UtcNow.Ticks,
                Status = TrashbinStatus.DangHoatDong.ToDisplayString(),
                AreaId = dto.AreaId,
                Location = dto.Location,
                Image = dto.Image,
                RestroomId = dto.RestroomId
            };

            await _TrashBinService.AddTrashBin(newTrashbin);

            return CreatedAtAction(nameof(GetById),
                new { id = newTrashbin.TrashBinId }, newTrashbin);
        }
        [HttpPut("toggle-status/{id}")]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            var trashbin = await _TrashBinService.GetTrashBinById(id);
            if (trashbin == null)
                return NotFound(new { message = "Thùng rác không tồn tại" });

            // Đảo trạng thái
            if (trashbin.Status == RoleStatus.DangHoatDong.ToDisplayString())
                trashbin.Status = RoleStatus.NgungHoatDong.ToDisplayString();
            else
                trashbin.Status = RoleStatus.DangHoatDong.ToDisplayString();

            await _TrashBinService.UpdateTrashBin(trashbin);

            return Ok(new
            {
                message = "Cập nhật trạng thái thành công",
                newStatus = trashbin.Status
            });
        }
        [HttpGet("with-sensors")]
        public async Task<IActionResult> GetTrashBinsWithSensors()
        {
            var data = await _TrashBinService.GetTrashBinsWithSensorsAsync();

            return Ok(new ResponseDTO(200, "Lấy dữ liệu thành công", data));
        }
        #endregion
    }
}
