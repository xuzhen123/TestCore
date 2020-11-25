using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace XZ.My.Controllers
{
    //[ApiController]
    [Route("api/[controller]")]
    public class HttpTestController : ControllerBase
    {
        [HttpGet("get")]
        public string Get()
        {
            return "这是一个HttpClient的测试";
        }
    }
}