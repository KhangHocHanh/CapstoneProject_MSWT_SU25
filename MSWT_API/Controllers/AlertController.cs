using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Services.IServices;
using MSWT_Services.Services;
using static MSWT_BussinessObject.Enum.Enum;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

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
        [HttpPost]
        public async Task<ActionResult<Alert>> Create([FromBody] AlertRequestDTO dto)
        {
            var newAlert = new Alert
            {
                AlertId = "AL" + DateTime.UtcNow.Ticks,
                TrashBinId = dto.TrashBinId,
                TimeSend = DateTime.UtcNow,
                Status = "Cần được xử lý",
                UserId = null
            };
           

            await _alertService.CreateAlertAsync(newAlert);

            return CreatedAtAction(nameof(GetById),
                new { id = newAlert.TrashBinId }, newAlert);
        }
        #endregion
    }
}
