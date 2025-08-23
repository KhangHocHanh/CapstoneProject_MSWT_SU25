using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Services.IServices;
using MSWT_Services.Services;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_API.Controllers
{
    [Route("api/areas")]
    [ApiController]
    public class AreaController : Controller
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        #region CRUD Category
        [HttpPost]
        public async Task<ActionResult<AreaResponseDTO>> CreateArea([FromBody] AreaRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdArea = await _areaService.CreateAreaAsync(request);
            return Ok(createdArea);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaResponseDTO>>> GetAll()
        {
            return Ok(await _areaService.GetAllAreasAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetById(string id)
        {
            var area = await _areaService.GetAreaById(id);
            if (area == null)
                return NotFound();
            return Ok(area);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _areaService.DeleteArea(id);
                return NoContent(); // 204 No Content if success
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 Bad Request if error
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArea(string id, [FromBody] AreaUpdateRequestDTO requestDto)
        {
            try
            {
                await _areaService.UpdateArea(id, requestDto);
                return NoContent(); // Successful update, no content returned
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Or handle with more detailed error logic
            }
        }

        [HttpPut("{areaId}/{floorId}")]
        public async Task<IActionResult> AddFloorToArea(string areaId, string buildingId)
        {
            try
            {
                var result = await _areaService.AddBuildingToArea(areaId, buildingId);
                if (!result)
                    return BadRequest("Failed to add building to area.");

                return Ok("Floor successfully added to area.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

    }
}
