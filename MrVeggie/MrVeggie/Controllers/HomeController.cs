using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Contexts;
using MrVeggie.Models;

namespace MrVeggie.Controllers {

    public class HomeController : Controller {

        Selecao selecao;

        public HomeController(UtilizadorContext context_u, ReceitaContext context_r) {
            selecao = new Selecao(context_r, null, null, context_u);
        }

        public IActionResult Index() {
            Receita[] receitas;

            if (User.Identity.IsAuthenticated) {
                string email = User.Identity.Name;
                receitas = selecao.getSugestoes(email);
            }
            else receitas = selecao.getSugestoes();

            return View(receitas);
        }

        [HttpGet]
        public IActionResult AboutUs() {

            return View();
        }

    }
}