using BLC;
using BLC.LoginComponent;
using BLC.ProfileComponent;
using Entities;
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
        [HttpGet("check-credentials")]
        public IActionResult Check_Credentials(CredentialsDto credentials)
        {
            
            var response = _blc.Authenticate(credentials);
            return Ok(new { response });
        }

        [HttpGet("login-user")]
        public IActionResult LoginUser(string i__UserName, string i__Password, string i__ClientType, bool i__IsFirstLogin, string sessionId)
        {
            
            CredentialsDto credentials = new CredentialsDto()
            {
                Username = i__UserName,
                Password = i__Password,
                ClientType = i__ClientType,
                IsFirstLogin = i__IsFirstLogin,
                SessionID = sessionId
            };
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
            var IsFirstLogin = user.IsFirstLogin;
            var IsAuthenticated = user.IsAuthenticated;
            Dictionary<string, string> oServerResponse = loginResponse.AllKeys.ToDictionary(key => key, key => loginResponse[key]);

            return Ok( new { IsAuthenticated, IsFirstLogin, oServerResponse });
        }

        [HttpGet("get-user")]
        public IActionResult GetUser(string i__UserName, string i__Password, string i__ClientType, bool i__IsFirstLogin, string sessionId)
        {
            CredentialsDto credentials = new CredentialsDto()
            {
                Username = i__UserName,
                Password = i__Password,
                ClientType = i__ClientType,
                IsFirstLogin = i__IsFirstLogin,
                SessionID = sessionId
            };

            _blcProfile.DQ_GetUserAccount(credentials);
            return Ok();
        }

    }
}
