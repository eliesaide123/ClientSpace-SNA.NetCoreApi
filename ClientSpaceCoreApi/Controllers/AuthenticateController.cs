using BLC;
using BLC.LoginComponent;
using BLC.ProfileComponent;
using Entities;
using Entities.IActionResponseDTOs;
using Entities.JSONResponseDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace ClientSpaceCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly BusinessLogicLogin _blc;
        private readonly BusinessLogicProfile _blcProfile;
        public AuthenticateController(IHttpContextAccessor _contextAccessor) {
            _blc = new BusinessLogicLogin(_contextAccessor);
            _blcProfile = new BusinessLogicProfile(_contextAccessor);
        }

        [HttpPost("login-user")]
        public ActionResult<LoginUserResponse> LoginUser([FromBody] CredentialsDto credentials)
        {
            var user = _blc.Authenticate(credentials);
            var errors_Response = _blc.IsFirstLogin(user) as Dictionary<string,string>;

            return Ok( new { user, errors_Response });
        }

        [HttpPost("get-user")]
        public IActionResult GetUser([FromBody] CredentialsDto credentials)
        {

            var responseObject = _blcProfile.DQ_GetUserAccount(credentials);
            var data = JsonConvert.DeserializeObject<GetUserAccountResponse>(responseObject);
            return Ok(data);
        }

        [HttpPost("get-client-info")]
        public IActionResult GetClientInfo([FromBody] DoOpMainParams parameters)
        {
            var responseObject = JsonConvert.DeserializeObject<GetClientInfoResponseDto>(_blcProfile.DQ_GetClientInfo(parameters));
           
            return Ok(responseObject);
        }
    }
}
