using BLC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientSpaceCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly BusinessLogic _blc;
        private readonly HttpContextAccessor _contextAccessor = new HttpContextAccessor();
        public SessionController() {
            _blc = new BusinessLogic(_contextAccessor);
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
