using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Contexts;
using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;

namespace MrVeggie.Controllers {


    public class AdminViewController : Controller {

        private Admin admin;

        public AdminViewController(IngredienteContext context_i, UtilizadorContext context_u, ReceitaContext context_r, UtensilioContext context_uten ,OperacaoContext context_op) {
            admin = new Admin(context_i, context_u, context_r, context_op, context_uten);

        }


            public IActionResult Index() {
            return View();
        }

        public IActionResult NewIngrediente() {
            return View();
        }

        public IActionResult Estatisticas() {
            Estatistica est = admin.getEstatistica();

            return View(est);
        }

        public IActionResult NewReceita() {
            List<Utensilio> utensilios = admin.getUtensilios();

            return View(new Tuple<Receita, List<Utensilio>>(null, utensilios));
        }


        public IActionResult registaIngrediente(string nome, string url_imagem) {

            admin.registaIngrediente(nome, url_imagem);

            return RedirectToAction("Index", "AdminView");
        }



        public IActionResult registaReceita(string nome, string desc, int dificuldade, float tempo_conf, int calorias, int n_pessoas, string url_imagem) {

            admin.registaReceita(nome, desc, dificuldade, tempo_conf, calorias, n_pessoas, url_imagem);

            return RedirectToAction("Index", "AdminView");
        }

        [HttpPost]
        public IActionResult registaPasso(Tuple<Passo, List<Operacao>, List<Receita>, int, int> t)
        {

            //admin.registaPasso();

            return RedirectToAction("NewPasso", "AdminView", t );
        }

        public IActionResult NewPasso(Tuple<Passo, List<Operacao>, List<Receita>, int, int> t){
          
           return View(new Tuple<Passo, List<Operacao>, List<Receita>,int,int>(new Passo(), admin.getOperacoes(), admin.getReceitas(), t.Item4, t.Item5));
        }
    }
}