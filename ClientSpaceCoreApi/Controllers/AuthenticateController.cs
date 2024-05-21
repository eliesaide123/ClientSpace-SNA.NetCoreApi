using BLC;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientSpaceCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly BusinessLogic _blc;
        public AuthenticateController() {
            _blc = new BusinessLogic();
        }
        [HttpGet]
        public ActionResult Check_Credentials(string i__UserName, string i__Password, string i__ClientType, bool i__IsFirstLogin)
        {
            CredentialsDto credentials = new CredentialsDto()
            {
                Username = i__UserName,
                Password = i__Password,
                ClientType = i__ClientType,                
                IsFirstLogin = i__IsFirstLogin
            };
            var response = _blc.Authenticate(credentials);
            return Ok(response);
        }
    }
}
