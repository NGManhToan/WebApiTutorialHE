using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebApiTutorialHE.Models.Notification;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Service;

namespace WebApiTutorialHE.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SharingHubController : ControllerBase
    {
        private readonly IHubContext<SharingHub> _hubContext;

        public SharingHubController(IHubContext<SharingHub> hubContext)
        {
            _hubContext = hubContext;
        }

		    [HttpPost("send")]
		public async Task<IActionResult> SendNotification(string connectionId, [FromBody] ENotificationListModel notification)
		{
			await _hubContext.Clients.Client(connectionId).SendAsync("SharingProject", JsonConvert.SerializeObject(notification));

			return Ok();
		}

	}
}