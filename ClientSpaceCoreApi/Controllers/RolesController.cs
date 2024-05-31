using BLC.LoginComponent;
using BLC.RolesComponent;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ClientSpaceCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly BusinessLogicRoles _blcRoles;
        public RolesController(IHttpContextAccessor _contextAccessor)
        {
            _blcRoles = new BusinessLogicRoles(_contextAccessor);
        }

            [HttpGet("check-roles")]
        public IActionResult Check_Roles(string i__UserName, string i__Password, string i__ClientType, bool i__IsFirstLogin, string sessionId)
        {
            CredentialsDto credentials = new CredentialsDto()
            {
                Username = i__UserName,
                Password = i__Password,
                ClientType = i__ClientType,
                IsFirstLogin = i__IsFirstLogin,
                SessionID = sessionId
            };

            var response = _blcRoles.DQ_CheckRoles(credentials);
            var isError = JsonConvert.DeserializeObject(response);

            var setRoleResponse = _blcRoles.SetRole(sessionId, "PH");
            return Ok(isError);
        }
    }
}
