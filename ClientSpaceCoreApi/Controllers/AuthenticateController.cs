using AutoMapper;
using BLC;
using BLC.LoginComponent;
using BLC.ProfileComponent;
using DAL.LoginComponent;
using Entities;
using Entities.IActionResponseDTOs;
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
        public AuthenticateController(IHttpContextAccessor _contextAccessor, IMapper _mapper, ILoginDAL DAL) {
            _blc = new BusinessLogicLogin(_contextAccessor, _mapper, DAL);
            _blcProfile = new BusinessLogicProfile(_contextAccessor, _mapper);
        }

        //test
        [HttpPost("login-user")]
        public ActionResult<LoginUserResponse> LoginUser([FromBody] CredentialsDto credentials)
        {
            var login_Response = _blc.Authenticate(credentials);

            return Ok(login_Response);
        }

        [HttpPost("get-user")]
        public ActionResult<GetUserAccountResponse> GetUser([FromBody] CredentialsDto credentials)
        {

            var data = _blcProfile.DQ_GetUserAccount(credentials);
            return Ok(data);
        }

        [HttpPost("get-client-info")]
        public ActionResult<GetClientInfoResponse> GetClientInfo([FromBody] DoOpMainParams parameters)
        {
            var responseObject = _blcProfile.DQ_GetClientInfo(parameters);
           
            return Ok(responseObject);
        }
    }
}
