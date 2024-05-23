using BLC;
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
        private readonly BusinessLogic _blc;
        private readonly HttpContextAccessor _contextAccessor;
        public AuthenticateController() {
            _blc = new BusinessLogic(_contextAccessor);
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
            CredentialsDto user = JsonConvert.DeserializeObject<CredentialsDto>(Check_Credentials(credentials).ToString());

            var response = _blc.IsFirstLogin(user);
            var IsFirstLogin = user.IsFirstLogin;

            return Ok( new {IsFirstLogin, response});
        }
    }
}
