using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientSpaceCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        [HttpGet("session-id")]
        public IActionResult GetSessionId()
        {
            var sessionId = HttpContext.Session.Id;
            return Ok(new { sessionId });
        }
    }
}
