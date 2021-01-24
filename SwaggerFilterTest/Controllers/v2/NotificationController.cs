using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerFilterTest.Controllers.v2
{
	[ApiVersion("2.0")]
	[Route("api/v{version:apiVersion}/notification")]
	[ApiController]
	[ApiExplorerSettings(GroupName = "v2-conA")]
	public class NotificationController : ControllerBase
	{
		[HttpPost("send-notification")]
		[SwaggerOperation(Tags = new[] { "ConA - Notification", "ConC - Notification" })]
		public ActionResult SendNotification([FromBody] string userId)
		{
			return Ok($"{userId} V2");
		}
	}
}
