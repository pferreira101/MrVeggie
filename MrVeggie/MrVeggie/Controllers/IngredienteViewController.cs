using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Contexts;
using MrVeggie.Models;
using MrVeggie.Models.Auxiliary;
using MrVeggie.Shared;

namespace MrVeggie.Controllers {

    [Route("[controller]/[action]")]
    public class IngredienteViewController : Controller {

        private Selecao selecao;

        public IngredienteViewController(IngredienteContext context_i, IngredientesPassoContext context_ip, UtilizadorContext context_u, AgendaContext context_a) {
            selecao = new Selecao(null, context_i, context_ip, context_u, context_a);
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


        [HttpPost]
        public void ConfigInicial([FromBody] int[] ids) {
            selecao.setUserIngrPrefs(ids, User.Identity.Name);

        }
    }
}