using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using WebApiTutorialHE.Models.Notification;

namespace WebApiTutorialHE.Service
{
	public class SharingHub : Hub
	{
		private readonly IWebHostEnvironment _env;
		private readonly IHubContext<SharingHub> _hubContext;
		private readonly IConfiguration _configuration;

		public SharingHub(IWebHostEnvironment env,
			IHubContext<SharingHub> hubContext,
			IConfiguration configuration)
		{
			_env = env;
			_hubContext = hubContext;
			_configuration = configuration;
		}

		public async Task Connect(string token, string connectionIdOld)
		{
			var userId = GetUserId(token);

			if (!String.IsNullOrWhiteSpace(userId))
			{
				var userName = $"user_eudcation_{userId}";

				if (!String.IsNullOrWhiteSpace(connectionIdOld))
				{
					await _hubContext.Groups.RemoveFromGroupAsync(connectionIdOld, userName);
				}

				await _hubContext.Groups.AddToGroupAsync(Context.ConnectionId, userName);

				await base.OnConnectedAsync();

				Console.WriteLine("Kết nối oke: " + Context.ConnectionId);
			}
		}

		public async Task EducationSendMessOnUser(List<int> toUserIds, ENotificationListModel notification)
		{
			var tasks = new List<Task>();

			foreach (var toUserId in toUserIds)
			{
				var userName = $"user_eudcation_{toUserId.ToString()}";

				tasks.Add(_hubContext.Clients.Group(userName).SendAsync("SharingProject", JsonConvert.SerializeObject(notification)));
			}

			await Task.WhenAll(tasks);
		}

		public string GetUserId(string token)
		{
			if (!String.IsNullOrWhiteSpace(token))
			{
				token = token.Replace("Bearer ", "");

				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes(_configuration["JWTSettings:Key"]);

				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);

				var jwtToken = (JwtSecurityToken)validatedToken;

				return jwtToken.Claims.First(x => x.Type == "uid").Value;
			}

			return "";
		}
	}
}
