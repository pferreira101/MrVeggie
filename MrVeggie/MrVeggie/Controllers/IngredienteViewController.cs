using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            List<Boolean> nPossui = new List<bool>();
            if (User.Identity.IsAuthenticated)
            {
                nPossui = selecao.getFavs(User.Identity.Name);
            }
            
            

            return View(new Tuple<IEnumerable<MrVeggie.Models.Ingrediente>, List<Boolean>>(ingredientes,nPossui));
        }

        [HttpGet]
        public IActionResult ConfigInicial() {
            Ingrediente[] ingredientes = selecao.getIngredientes();

            return View(ingredientes);
        }


        [HttpPost]
        public IActionResult ConfigInicial([FromBody] int[] ids) {
            selecao.setUtilizadorIngredientesPref(ids, User.Identity.Name);
            return Content(Url.Action("Index","Home"));
        }



        [HttpPost]
        public void AdicionaIngredienteFavoritos([FromBody] int id_ingrediente) {
            selecao.adicionaIngredienteFavoritos(id_ingrediente, User.Identity.Name);
        }

        [HttpPost]
        public void removeIngredienteFavoritos([FromBody] int id_ingrediente)
        {
            selecao.removeIngredienteFavoritos(id_ingrediente, User.Identity.Name);
        }
    }
}