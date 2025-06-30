using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Services.IServices;
using MSWT_Services.Services;

namespace MSWT_API.Controllers
{
    [Route("api/restrooms")]
    [ApiController]
    public class RestroomController : Controller
    {
        private readonly IRestroomService _restroomService;

        public RestroomController(IRestroomService restroomService)
        {
            _restroomService = restroomService;
        }

        #region CRUD Category
        [HttpPost]
        public async Task<ActionResult<RestroomResponseDTO>> AddRestroom([FromBody] RestroomRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdFloor = await _restroomService.AddRestroom(request);
            return Ok(createdFloor);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restroom>>> GetAll()
        {
            return Ok(await _restroomService.GetAllRestrooms());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restroom>> GetById(string id)
        {
            var restroom = await _restroomService.GetRestroomById(id);
            if (restroom == null)
                return NotFound();
            return Ok(restroom);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RestroomResponseDTO>> UpdateRestroom(string id, [FromBody] RestroomRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedRestroom = await _restroomService.UpdateRestroom(id, request);
                return Ok(updatedRestroom);
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
                await _restroomService.DeleteRestroom(id);
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
