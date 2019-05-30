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


        [HttpPost]
        public void ConfigInicial(string ids) {
            string[] ids_ingrediente_str = ids.Split(",");

            int[] ids_ingrediente = new int[ids_ingrediente_str.Count()];

            for (int i = 0; i < ids_ingrediente_str.Count(); i++) {
                ids_ingrediente[i] = Int32.Parse(ids_ingrediente_str[i]);
            }

            // adicionar......
        }
    }
}