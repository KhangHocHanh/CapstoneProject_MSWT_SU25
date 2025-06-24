using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_Services.IServices;

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
        #endregion
    }
}
