﻿using AutoMapper;
using BLC;
using BLC.LoginComponent;
using DAL.LoginComponent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientSpaceCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly BusinessLogicLogin _blc;
        public SessionController(IHttpContextAccessor _contextAccessor, IMapper _mapper, ILoginDAL DAL) {
            _blc = new BusinessLogicLogin(_contextAccessor, _mapper, DAL);
        }
        [HttpGet("session-id")]
        public IActionResult GetSessionId()
        {
            var sessionId = HttpContext.Session.Id;
            _blc.GetSession(sessionId);
            return Ok(new { sessionId });
        }
    }
}
