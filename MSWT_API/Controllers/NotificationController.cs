using Microsoft.AspNetCore.Mvc;
using MSWT_Services;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;

namespace MSWT_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequest request)
        {
            try
            {
                var result = await _notificationService.SendNotificationAsync(
                    request.Token,
                    request.Title,
                    request.Body,
                    request.Data
                );

                return Ok(new { Success = true, MessageId = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Error = ex.Message });
            }
        }

        [HttpPost("send-multicast")]
        public async Task<IActionResult> SendMulticastNotification([FromBody] SendMulticastNotificationRequest request)
        {
            try
            {
                var result = await _notificationService.SendMulticastNotificationAsync(
                    request.Tokens,
                    request.Title,
                    request.Body,
                    request.Data
                );

                return Ok(new
                {
                    Success = true,
                    SuccessCount = result.SuccessCount,
                    FailureCount = result.FailureCount
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Error = ex.Message });
            }
        }

        [HttpPost("send-to-topic")]
        public async Task<IActionResult> SendToTopic([FromBody] SendTopicNotificationRequest request)
        {
            try
            {
                var result = await _notificationService.SendToTopicAsync(
                    request.Topic,
                    request.Title,
                    request.Body,
                    request.Data
                );

                return Ok(new { Success = true, MessageId = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Error = ex.Message });
            }
        }
    }
}
