using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MrVeggie.Controllers{

    [Route("[controller]/[action]")]
    public class AvaliacaoViewController : Controller {

        public IActionResult AvaliarReceita() {

            return View();
        }
    }
}