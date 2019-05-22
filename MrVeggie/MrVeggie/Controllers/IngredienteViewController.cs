using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Contexts;
using MrVeggie.Models;
using MrVeggie.Shared;

namespace MrVeggie.Controllers {

    [Route("[controller]/[action]")]
    public class IngredienteViewController : Controller {

        private Selecao selecao;

        public IngredienteViewController(IngredienteContext context) {
            selecao = new Selecao(null, context,null, null);
        }


        [HttpGet]
        public IActionResult showIngredientes() {
            Ingrediente[] ingredientes = selecao.getIngredientes();

            return View(ingredientes);
        }

        [HttpGet]
        public IActionResult ConfigInicial() {
            Ingrediente[] ingredientes = selecao.getIngredientes();

            return View(ingredientes);
        }
    }
}