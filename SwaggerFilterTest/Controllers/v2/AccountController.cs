using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Constants = SwaggerFilterTest.Constants;

namespace SwaggerFilterTest.Controllers.v2
{
	[Route("api/v{version:apiVersion}/account")]
	[ApiController]
	[ApiVersion("2.0")]
	public class AccountController : ControllerBase
	{
		[HttpGet("get-user-details")]
		[SwaggerOperation(Tags = new[] { Constants.ApiConsumerTagNameConAAccount })]
		public ActionResult GetUserDetails([FromQuery]string userId)
		{
			return Ok($"{userId} V2");
		}
	}
}
