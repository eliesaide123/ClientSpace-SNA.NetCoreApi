using BLC.ProfileComponent;
using Entities;
using Entities.IActionResponseDTOs;
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

        [HttpPost("get-portfolio")]
        public ActionResult<GetPortfolioResponse> GetPortolio([FromBody] DoOpMainParams parameters)
        {
            var response = _blcProfile.GetPortfolio(parameters);
            return Ok(response);
        }
    }
}
