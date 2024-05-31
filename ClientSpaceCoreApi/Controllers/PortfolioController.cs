using BLC.ProfileComponent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientSpaceCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly BusinessLogicProfile _blcProfile;
        public PortfolioController(IHttpContextAccessor contextAccessor) {
            _blcProfile = new BusinessLogicProfile(contextAccessor);
        }

        [HttpGet("get-portfolio")]
        public IActionResult GetPortolio(string sessionId, int gridSize, string direction, string roleId)
        {
            var response = _blcProfile.GetPortfolio(sessionId, gridSize, direction, roleId);
            return Ok(response);
        }
    }
}
