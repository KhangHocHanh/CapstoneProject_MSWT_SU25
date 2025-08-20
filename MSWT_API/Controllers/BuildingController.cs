using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Services.IServices;

namespace MSWT_API.Controllers
{
    [Route("api/buildings")]
    [ApiController]
    public class BuildingController : Controller
    {
            private readonly IBuildingService _buildingService;

            public BuildingController(IBuildingService floorService)
            {
            _buildingService = floorService;
            }

            #region CRUD Category
            [HttpPost]
            public async Task<ActionResult<BuildingResponseDTO>> CreateBuilding([FromBody] BuildingRequestDTO request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                    
                var createdBuilding = await _buildingService.CreateBuildingAsync(request);
                return Ok(createdBuilding);
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<BuildingResponseDTO>>> GetAll()
            {
                return Ok(await _buildingService.GetAllBuildings());
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<BuildingResponseDTO>> GetById(string id)
            {
                var building = await _buildingService.GetBuildingById(id);
                if (building == null)
                    return NotFound();
                return Ok(building);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateFloor(string id, [FromBody] BuildingRequestDTO request)
            {
                try
                {
                    var result = await _buildingService.UpdateBuilding(id, request);
                    if (!result)
                    {
                        return NotFound($"Building with ID {id} not found.");
                    }

                    return Ok(); // or Ok() if you prefer returning a success message
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
                    await _buildingService.DeleteBuilding(id);
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
