using WebApiTutorialHE.Models;
using WebApiTutorialHE.Service;
using Microsoft.AspNetCore.Mvc;
using WebApiTutorialHE.Models.Mail;
using WebApiTutorialHE.Service.Interface;

namespace MailKitDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mail;

        public MailController(IMailService mail)
        {
            _mail = mail;
        }

        [HttpPost("sendmail")]
        public async Task<IActionResult> SendMailAsync([FromForm]MailDataWithAttachments mailData)
        {
            bool result = await _mail.SendMail(mailData, new CancellationToken());

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
            }
        }

    }
}