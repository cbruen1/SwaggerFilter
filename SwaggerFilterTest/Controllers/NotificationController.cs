using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerFilterTest.Controllers
{
	[Route("api/notification")]
	[ApiController]
	[ApiExplorerSettings(GroupName = "v1")]
	public class NotificationController : ControllerBase
	{
		[HttpPost("send-notification")]
		public ActionResult SendNotification([FromBody]string userId)
		{
			return Ok($"{userId} V1");
		}
	}
}
