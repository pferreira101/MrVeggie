using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Contexts;
using MrVeggie.Models;

namespace MrVeggie.Controllers {


    public class AdminViewController : Controller {

        private Admin admin;

        public AdminViewController(IngredienteContext context_i) {
            admin = new Admin(context_i);
        }


        public IActionResult Index() {
            return View();
        }

        public IActionResult NewIngrediente() {
            return View();
        }

        public IActionResult Estatisticas() {
            return View();
        }

        public IActionResult NewReceita() {
            return View();
        }


        public IActionResult registaIngrediente(string nome, string url_imagem) {

            admin.registaIngrediente(nome, url_imagem);

            return RedirectToAction("Index", "AdminView");
        }
    }
}