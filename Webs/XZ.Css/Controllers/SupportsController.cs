using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace XZ.Css.Controllers
{
    [Authorize(Roles = "Admin,1e81a56a-d144-11ea-a061-de15bc72dcfe")]
    public class SupportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}