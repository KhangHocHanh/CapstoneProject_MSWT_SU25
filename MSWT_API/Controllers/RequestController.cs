using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_Services.IServices;
using MSWT_BussinessObject.RequestDTO;
using System.Security.Claims;
using MSWT_BussinessObject;
using MSWT_BussinessObject.ResponseDTO;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using MSWT_BussinessObject.Enum;
using static MSWT_BussinessObject.Enum.Enum;



namespace MSWT_API.Controllers
{
    [Route("api/request")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IRequestService _requestService;
        private readonly IMapper _mapper;

        public RequestController(IRoleService roleService,IRequestService requestService, IMapper mapper )
        {
            _roleService = roleService;
            _mapper = mapper;
            _requestService = requestService;
        }


        #region CRUD Category

        // GET api/requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetAsync()
        {
            var requests = await _requestService.GetAllRequests();
            return Ok(requests);
        }

        // GET api/requests/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetByIdAsync(string id)
        {
            var request = await _requestService.GetRequestById(id);
            return request is null ? NotFound() : Ok(request);
        }

        // DELETE api/requests/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _requestService.DeleteRequest(id);
                return NoContent(); // 204 No Content if success
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 Bad Request if error
            }
        }

        // POST api/requests
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RequestDTO.RequestCreateDto dto)
        {
            var userId = User.FindFirstValue("User_Id");

            var allRequests = await _requestService.GetAllRequests();
            var nextId = "RQ" + DateTime.UtcNow.Ticks;

            var request = _mapper.Map<Request>(dto);
            request.RequestId = nextId;
            request.UserId = userId;
            request.Status = RequestStatusHelper.ToStringStatus(RequestStatusEnum.DaGui);
            request.RequestDate = dto.RequestDate ?? DateOnly.FromDateTime(DateTime.Now); // Ngày gửi yêu cầu, nếu không có thì lấy ngày hiện tại
            request.ResolveDate = null; // Chưa giải quyết

            await _requestService.AddRequest(request);

            var response = new ResponseDTO(
          Const.SUCCESS_CREATE_CODE,
          Const.SUCCESS_CREATE_MSG,
          null 
      );

            return Ok(response);
        }

        // PUT api/request/status
        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] RequestUpdateStatusDto dto)
        {
            var request = await _requestService.GetRequestById(dto.RequestId);
            if (request is null)
                return NotFound(new ResponseDTO(Const.ERROR_EXCEPTION, "Request không tồn tại"));

            // Chuyển trạng thái hiện tại từ string → enum
            var currentStatus = RequestStatusHelper.ToEnum(request.Status);

            // Chặn hủy khi đã xử lý
            if (dto.Status == RequestStatusEnum.DaHuy && currentStatus == RequestStatusEnum.DaXuLy)
                return BadRequest(new ResponseDTO(Const.ERROR_EXCEPTION, "Không thể hủy Request đã được xử lý"));

            // Cập nhật
            request.Status = RequestStatusHelper.ToStringStatus(dto.Status);

            // Nếu chuyển sang Đã xử lý → set ResolveDate
            if (dto.Status == RequestStatusEnum.DaXuLy && request.ResolveDate is null)
                request.ResolveDate = DateOnly.FromDateTime(DateTime.UtcNow);
            // Nếu chuyển sang Đã xử lý → set ResolveDate
            if (dto.Status == RequestStatusEnum.DaHuy && request.ResolveDate is null)
                request.ResolveDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await _requestService.UpdateRequest(request);

            return Ok(new ResponseDTO(Const.SUCCESS_UPDATE_CODE, "Cập nhật trạng thái thành công", request));
        }

        // PUT api/request/cancel/{id}
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelRequest(string id)
        {
            var request = await _requestService.GetRequestById(id);
            if (request == null)
                return NotFound(new ResponseDTO(Const.ERROR_EXCEPTION, "Request không tồn tại"));

            var currentStatus = RequestStatusHelper.ToEnum(request.Status ?? "");

            if (currentStatus == RequestStatusEnum.DaXuLy)
                return BadRequest(new ResponseDTO(Const.ERROR_EXCEPTION, "Không thể hủy Request đã được xử lý"));

            request.Status = RequestStatusHelper.ToStringStatus(RequestStatusEnum.DaHuy);
            request.ResolveDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await _requestService.UpdateRequest(request);

            return Ok(new ResponseDTO(Const.SUCCESS_UPDATE_CODE, "Hủy Request thành công", request));
        }

        #endregion

    }
}
