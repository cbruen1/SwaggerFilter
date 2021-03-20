using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerFilterTest.Controllers.v2
{
	[Route("api/v{version:apiVersion}/notification")]
	[ApiController]
	[ApiVersion("2.0")]
	public class NotificationController : ControllerBase
	{
		[HttpPost("send-notification")]
		[SwaggerOperation(Tags = new[] { Constants.ApiConsumerTagNameConANotification, Constants.ApiConsumerTagNameConCNotification })]
		public ActionResult SendNotification([FromBody] string userId)
		{
			return Ok($"{userId} V2");
		}
	}
}
