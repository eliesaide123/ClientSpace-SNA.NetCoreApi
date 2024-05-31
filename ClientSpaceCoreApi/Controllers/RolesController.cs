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

            [HttpPost("check-roles")]
        public IActionResult Check_Roles([FromBody] DoOpMainParams parameters)
        {
            var response = _blcRoles.DQ_CheckRoles(parameters.Credentials);
            var isError = JsonConvert.DeserializeObject(response);

            var setRoleResponse = _blcRoles.SetRole(parameters.Credentials.SessionID, parameters.RoleID);
            return Ok(isError);
        }
    }
}
