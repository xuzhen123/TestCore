using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using XZ.Css.HttpClients;
using XZ.Css.Models;

namespace XZ.Css.Controllers
{
    //[EnableCors("abc")] //设置action 允许跨域请求 其策略设置为abc
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + "," + CookieAuthenticationDefaults.AuthenticationScheme)]//同时启用cookie和jwt两种认证方式
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index([FromServices]HttpClientApi1 httpClient)
        {
            var user = User.Identity.Name;

            bool IsAuth = User.Identity.IsAuthenticated;

            //var asdf = await httpClient.GetAsync("https://localhost:5001/api/test/abc",new Dictionary<string, string>());

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //[AutoValidateAntiforgeryToken]//开启单个 Action 的防跨站脚本攻击
        //[DisableCors]//不允许使用跨域请求策略
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
