using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerFilterTest.Controllers.v2
{
	[Route("api/v{version:apiVersion}/account-admin")]
	[ApiController]
	[ApiVersion("2.0")]
	public class AccountAdminController : ControllerBase
	{
		[HttpPost("verify")]
		[SwaggerOperation(Tags = new[] { Constants.ApiConsumerTagNameConBAccountAdmin, Constants.ApiConsumerTagNameConCAccountAdmin })]
		public ActionResult Verify([FromBody] string userId)
		{
			return Ok($"{userId} V2");
		}
	}
}
