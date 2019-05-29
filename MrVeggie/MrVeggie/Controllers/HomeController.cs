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

        /*
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
        */
        private Sugestao sugestao;


        public HomeController(ReceitaContext context_r, UtilizadorContext context_u, IngredientesPassoContext context_ip) {
            sugestao = new Sugestao(context_r, context_u, context_ip, null);
        }

        public IActionResult Index() {
            List<Receita> sugestoes = new List<Receita>();

            if (User.Identity.IsAuthenticated) {
                string email = User.Identity.Name;
                sugestoes = sugestao.getSugestoes(email);
            }
            else sugestoes = sugestao.getSugestoes();

            return View(sugestoes);
        }
         

        [HttpGet]
        public IActionResult AboutUs() {

            return View();
        }

    }
}