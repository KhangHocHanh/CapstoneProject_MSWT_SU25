using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Services.IServices;

namespace MSWT_API.Controllers
{
    [Route("api/floors")]
    [ApiController]
    public class FloorController : Controller
    {
            private readonly IFloorService _floorService;

            public FloorController(IFloorService floorService)
            {
            _floorService = floorService;
            }

            #region CRUD Category
            [HttpPost]
            public async Task<ActionResult<FloorResponseDTO>> CreateFloor([FromBody] FloorRequestDTO request)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                    
                var createdFloor = await _floorService.CreateFloorAsync(request);
                return Ok(createdFloor);
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<FloorResponseDTO>>> GetAll()
            {
                return Ok(await _floorService.GetAllFloors());
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<FloorResponseDTO>> GetById(string id)
            {
                var floor = await _floorService.GetFloorById(id);
                if (floor == null)
                    return NotFound();
                return Ok(floor);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateFloor(string id, [FromBody] FloorRequestDTO request)
            {
                try
                {
                    var result = await _floorService.UpdateFloor(id, request);
                    if (!result)
                    {
                        return NotFound($"Floor with ID {id} not found.");
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
                    await _floorService.DeleteFloor(id);
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
