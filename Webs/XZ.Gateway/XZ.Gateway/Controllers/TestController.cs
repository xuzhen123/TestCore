using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace XZ.Gateway.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public IActionResult Abc()
        {
            return Content("这是Gateway的返回");
        }
    }
}
