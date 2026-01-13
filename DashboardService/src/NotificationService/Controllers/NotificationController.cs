using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NotificationService.Model;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "NotificationAPI")] // Require JWT authentication for all actions
    public class NotificationController : ControllerBase
    {
        [HttpPost("sendemail")]
        public IActionResult SendEmail([FromBody] EmailNotificationDto emailNotification)
        {
            if (string.IsNullOrWhiteSpace(emailNotification.To) || string.IsNullOrWhiteSpace(emailNotification.Subject) || string.IsNullOrWhiteSpace(emailNotification.Body))
            {
                return BadRequest("Missing required email fields.");
            }
            // TODO: Integrate with email sending service here
            return Ok(new { Success = true, Type = "Email", To = emailNotification.To });
        }

        [HttpPost("sendsms")]
        public IActionResult SendSms([FromBody] SmsNotificationDto smsNotification)
        {
            if (string.IsNullOrWhiteSpace(smsNotification.To) || string.IsNullOrWhiteSpace(smsNotification.Message))
            {
                return BadRequest("Missing required SMS fields.");
            }
            // TODO: Integrate with SMS sending service here
            return Ok(new { Success = true, Type = "SMS", To = smsNotification.To });
        }

        [HttpPost("sendpush")]
        public IActionResult SendPush([FromBody] PushNotificationDto pushNotification)
        {
            if (string.IsNullOrWhiteSpace(pushNotification.To) || string.IsNullOrWhiteSpace(pushNotification.Title) || string.IsNullOrWhiteSpace(pushNotification.Body))
            {
                return BadRequest("Missing required push notification fields.");
            }
            // TODO: Integrate with push notification service here
            return Ok(new { Success = true, Type = "Push", To = pushNotification.To });
        }
    }

}
