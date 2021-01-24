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
	[Route("api/v{version:apiVersion}/account")]
	[ApiController]
	[ApiExplorerSettings(GroupName = "v2-conA")]
	public class AccountController : ControllerBase
	{
		[HttpGet("get-user-details")]
		[SwaggerOperation(Tags = new[] { "ConA - Account" })]
		public ActionResult GetUserDetails([FromQuery]string userId)
		{
			return Ok($"{userId} V2");
		}
	}
}
