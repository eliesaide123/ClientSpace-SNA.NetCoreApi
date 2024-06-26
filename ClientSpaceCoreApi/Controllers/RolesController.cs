﻿using BLC.LoginComponent;
using BLC.RolesComponent;
using Entities;
using Entities.IActionResponseDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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
        public ActionResult<CheckRolesResponse> Check_Roles([FromBody] CredentialsDto credentials)
        {
            var response = _blcRoles.DQ_CheckRoles(credentials) as CheckRolesResponse;

            _blcRoles.SetRole(credentials.SessionID, response?.SUCCESS.RoleID);
            return Ok(response);
        }
    }
}
