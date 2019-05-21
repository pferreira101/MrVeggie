using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Contexts;
using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;

namespace MrVeggie.Controllers {

    public class HomeController : Controller {

        private Sugestao sugestao;

        public HomeController(ReceitaContext context_r, UtilizadorContext context_u)
        {
            sugestao = new Sugestao(context_r, context_u);
        }

        public IActionResult Index() {
            return View(new HomeModel { sugestoes = sugestao.getReceitasSugeridas(1)});
        }

        [HttpGet]
        public IActionResult AboutUs() {

            return View();
        }

    }
}