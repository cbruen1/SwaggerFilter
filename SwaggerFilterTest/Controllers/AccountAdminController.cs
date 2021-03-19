using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerFilterTest.Controllers
{
	[Route("api/account-admin")]
	[ApiController]
	[ApiExplorerSettings(GroupName = Constants.ApiVersion1)]
	public class AccountAdminController : ControllerBase
	{
		[HttpPost("verify")]
		public ActionResult Verify([FromBody]string userId)
		{
			return Ok($"{userId} V1");
		}
	}
}
