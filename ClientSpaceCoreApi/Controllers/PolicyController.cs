using BLC.PolicyComponent;
using BLC.RolesComponent;
using Entities;
using Entities.IActionResponseDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientSpaceCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly BusinessLogicPolicy _blcPolicy;
        public PolicyController(IHttpContextAccessor _contextAccessor)
        {
            _blcPolicy = new BusinessLogicPolicy(_contextAccessor);
        }

        [HttpPost("get-policy-details")]
        public ActionResult<GetPolicyDetailsResponse> GetPolicyDetails([FromBody]DoOpMainParams paramters) {

            var response = _blcPolicy.DQ_GetPIPolicyDetails(paramters);
            return Ok(response);
        }
    }
}
