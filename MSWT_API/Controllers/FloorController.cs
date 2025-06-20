using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
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

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Floor>>> GetAll()
            {
                return Ok(await _floorService.GetAllFloors());
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Floor>> GetById(string id)
            {
                var floor = await _floorService.GetFloorById(id);
                if (floor == null)
                    return NotFound();
                return Ok(floor);
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
