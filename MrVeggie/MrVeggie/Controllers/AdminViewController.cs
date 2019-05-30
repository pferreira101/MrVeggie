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

        public AdminViewController(IngredienteContext context_i, UtilizadorContext context_u, ReceitaContext context_r) {
            admin = new Admin(context_i, context_u, context_r);
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
            return View();
        }


        public IActionResult registaIngrediente(string nome, string url_imagem) {

            admin.registaIngrediente(nome, url_imagem);

            return RedirectToAction("Index", "AdminView");
        }

        public IActionResult registaReceita(string nome, string desc, int dificuldade, float tempo_conf, int calorias, int n_pessoas, string url) {

            admin.registaReceita(nome, desc, dificuldade, tempo_conf, calorias, n_pessoas, url);

            return RedirectToAction("Index", "AdminView");
        }
    }
}