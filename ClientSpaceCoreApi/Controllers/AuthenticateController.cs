using BLC;
using BLC.LoginComponent;
using BLC.ProfileComponent;
using Entities;
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
        [HttpPost("check-credentials")]
        public IActionResult Check_Credentials([FromBody] CredentialsDto credentials)
        {
            
            var response = _blc.Authenticate(credentials);
            return Ok(new { response });
        }

        [HttpPost("login-user")]
        public IActionResult LoginUser([FromBody] CredentialsDto credentials)
        {
            var result = Check_Credentials(credentials) as OkObjectResult;
            if (result == null || result.Value == null)
            {
                return BadRequest("Invalid credentials response");
            }

            var responseData = result.Value;
            var jsonResponse = JsonConvert.SerializeObject(responseData);
            var responseObject = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            var response = responseObject.response;

            var user = JsonConvert.DeserializeObject<CredentialsDto>(response.ToString());

            var loginResponse = _blc.IsFirstLogin(user) as NameValueCollection;
            Dictionary<string, string> oServerResponse = loginResponse.AllKeys.ToDictionary(key => key, key => loginResponse[key]);

            return Ok( new { user , oServerResponse });
        }

        [HttpPost("get-user")]
        public IActionResult GetUser([FromBody] CredentialsDto credentials)
        {

            var responseObject = _blcProfile.DQ_GetUserAccount(credentials);
            var data = JsonConvert.DeserializeObject<GetUserAccountResponse>(responseObject);
            var userAccount = data.UserAccount;
            var questions = data.Questions;
            return Ok(new { userAccount, questions });
        }

        [HttpPost("get-client-info")]
        public IActionResult GetClientInfo([FromBody] DoOpMainParams parameters)
        {
            var responseObject = JsonConvert.DeserializeObject<GetClientInfoResponseDto>(_blcProfile.DQ_GetClientInfo(parameters));
           
            return Ok(responseObject);
        }
    }
}
