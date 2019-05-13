using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MrVeggie.Controllers {


    public class AgendaViewController : Controller {



        public IActionResult ShowAgenda() {
            return View();
        }
    }
}