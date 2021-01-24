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
	[ApiExplorerSettings(GroupName = "v2-conB")]
	public class AccountAdminController : ControllerBase
	{
		[HttpPost("verify")]
		[SwaggerOperation(Tags = new[] { "ConB - Account Admin", "ConC - Account Admin" })]
		public ActionResult Verify([FromBody] string userId)
		{
			return Ok($"{userId} V2");
		}
	}
}
