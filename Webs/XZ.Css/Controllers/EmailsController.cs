using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace XZ.Css.Controllers
{
    [Authorize(Roles = "Admin,55f593f8-d144-11ea-a061-de15bc72dcfe")]
    public class EmailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}