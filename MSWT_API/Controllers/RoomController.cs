using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Services.IServices;
using MSWT_Services.Services;

namespace MSWT_API.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        #region CRUD Category
        [HttpPost]
        public async Task<ActionResult<RoomResponseDTO>> AddRoom([FromBody] RoomRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdRoom = await _roomService.AddRoom(request);
            return Ok(createdRoom);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomResponseDTO>>> GetAll()
        {
            return Ok(await _roomService.GetAllRooms());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetById(string id)
        {
            var room = await _roomService.GetRoomById(id);
            if (room == null)
                return NotFound();
            return Ok(room);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoomResponseDTO>> UpdateRoom(string id, [FromBody] RoomRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedRoom = await _roomService.UpdateRoom(id, request);
                return Ok(updatedRoom);
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
                await _roomService.DeleteRoom(id);
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
