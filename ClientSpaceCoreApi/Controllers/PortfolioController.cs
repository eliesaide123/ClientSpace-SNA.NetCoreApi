using AutoMapper;
using BLC.ProfileComponent;
using DAL.ProfileComponent;
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
        public PortfolioController(IHttpContextAccessor contextAccessor, IMapper mapper, IProfileDAL DAL) {
            _blcProfile = new BusinessLogicProfile(contextAccessor, mapper, DAL);
        }

        [HttpPost("get-portfolio")]
        public ActionResult<GetPortfolioResponse> GetPortolio([FromBody] DoOpMainParams parameters)
        {
            var response = _blcProfile.DQ_GetPortfolio(parameters);
            return Ok(response);
        }
    }
}
