using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MrVeggie.Controllers {

    public class HomeController : Controller {

        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult AboutUs() {

            return View();
        }

    }
}