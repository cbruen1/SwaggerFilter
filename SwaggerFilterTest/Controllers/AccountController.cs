using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerFilterTest.Controllers
{
	[ApiController]
	[ApiExplorerSettings(GroupName = "v1")]
	public class AccountController : ControllerBase
	{
		[HttpGet("api/account/get-user-details")]
		public ActionResult GetUserDetails([FromQuery]string userId)
		{
			return Ok(new { UserId = userId, Name = "John", Surname = "Smith", Version = "V1" });
		}
	}
}
