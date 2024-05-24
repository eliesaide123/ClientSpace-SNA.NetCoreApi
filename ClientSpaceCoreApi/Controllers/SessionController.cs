using BLC;
using BLC.LoginComponent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientSpaceCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly BusinessLogicLogin _blc;
        public SessionController(IHttpContextAccessor _contextAccessor) {
            _blc = new BusinessLogicLogin(_contextAccessor);
        }
        [HttpGet("session-id")]
        public IActionResult GetSessionId()
        {
            var sessionId = HttpContext.Session.Id;
            _blc.GetSession(sessionId);
            return Ok(new { sessionId });
        }
    }
}
